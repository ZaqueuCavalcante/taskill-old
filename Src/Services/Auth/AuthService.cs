using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Taskill.Controllers;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Exceptions;
using Taskill.Settings;
using Task = System.Threading.Tasks.Task;
using static Taskill.Configs.AuthorizationConfigs;
using static Taskill.Extensions.ProjectExtensions;

namespace Taskill.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<Taskiller> _userManager;
    private readonly SignInManager<Taskiller> _signInManager;
    private readonly AuthSettings _authSettings;
    private readonly TaskillDbContext _context;

    public AuthService(
        UserManager<Taskiller> userManager,
        SignInManager<Taskiller> signInManager,
        AuthSettings authSettings,
        TaskillDbContext context
    ) {
        _userManager = userManager;
        _signInManager = signInManager;
        _authSettings = authSettings;
        _context = context;
    }

    public async Task CreateTaskiller(string email, string password)
    {
        var emailAlreadyUsed = await _context.Taskillers.AsNoTracking()
            .AnyAsync(t => t.Email == email);
        if (emailAlreadyUsed) throw new DomainException("Email already used.");

        var user = new Taskiller(email);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new DomainException(result.Errors.FirstOrDefault()?.ToString() ?? "Error on taskiller creation.");
        }

        var project = new Project(user.Id, DefaultProjectName);

        _context.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task<AccessTokenOut> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userName: email,
            password: password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (!result.Succeeded)
        {
            throw new DomainException("Login failed.");
        }

        var accessToken = await GenerateJwt(email);

        return new AccessTokenOut
        {
            access_token = accessToken,
            expires_in = _authSettings.JwtExpirationTimeInMinutes,
        };
    }

    private async Task<string> GenerateJwt(string email)
    {
        var user = await _userManager.FindByNameAsync(email);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user!.Id.ToString())
        };
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var identityClaims = new ClaimsIdentity(claims);

        var key = Encoding.ASCII.GetBytes(_authSettings.SecurityKey);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );

        var expirationTime = _authSettings.JwtExpirationTimeInMinutes;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _authSettings.Issuer,
            Audience = _authSettings.Audience,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(expirationTime),
            SigningCredentials = signingCredentials,
            Subject = identityClaims
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
