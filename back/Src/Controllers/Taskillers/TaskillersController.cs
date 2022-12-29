using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Services;
using static Taskill.Configs.AuthenticationConfigs;
using static Taskill.Configs.AuthorizationConfigs;
using static Taskill.Extensions.ProjectExtensions;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
public class TaskillersController : ControllerBase
{
    private readonly IAuthService _authService;

    public TaskillersController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Creates a new taskiller.
    /// </summary>
    [HttpPost("")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CreateTaskiller([FromBody] TaskillerIn data)
    {
        await _authService.CreateTaskiller(data.email, data.password);

        return NoContent();
    }

    /// <summary>
    /// Do login into app.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AccessTokenOut), 200)]
    public async Task<IActionResult> Login([FromBody] LoginIn data)
    {
        var tokens = await _authService.Login(data.email, data.password);

        return Ok(tokens);
    }

    /// <summary>
    /// Starts the OAuth flow, by building the url to Consent Screen and redirects to her.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("login-with-google")]
    [ProducesResponseType(302)]
    public IActionResult LoginWithGoogle([FromServices] SignInManager<Taskiller> signInManager)
    {
        var redirectUrl = Url.Action(nameof(HandleGoogleResponse));

        var properties = signInManager.ConfigureExternalAuthenticationProperties(GoogleScheme, redirectUrl);

        return new ChallengeResult(GoogleScheme, properties);
    }

    /// <summary>
    /// Handle the Google response.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("google-callback")]
    public async Task<IActionResult> HandleGoogleResponse(
        [FromServices] SignInManager<Taskiller> signInManager,
        [FromServices] UserManager<Taskiller> userManager,
        [FromServices] TaskillDbContext context)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();

        if (info == null)
            return RedirectToAction(nameof(LoginWithGoogle));

        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        var userEmail = info.Principal.FindFirst(ClaimTypes.Email)!.Value;
        if (result.Succeeded)
        {
            // Generate and return JWT?
            return Ok(userEmail);
        }

        var user = new Taskiller(userEmail);

        var identResult = await userManager.CreateAsync(user);
        if (identResult.Succeeded)
        {
            await userManager.AddToRoleAsync(user, TaskillerRole);

            var project = new Project(user.Id, DefaultProjectName);

            context.Add(project);
            await context.SaveChangesAsync();

            identResult = await userManager.AddLoginAsync(user, info);
            if (identResult.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                // Generate and return JWT?
                return Ok(userEmail);
            }
        }
        return Forbid();
    }

    /// <summary>
    /// Adds a taskiller to premium plan.
    /// </summary>
    [HttpPut("premium")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AddTaskillerToPremiumPlan([FromBody] PremiumPlanIn data)
    {
        await _authService.AddTaskillerToPremiumPlan(data.id, data.token);

        return NoContent();
    }
}
