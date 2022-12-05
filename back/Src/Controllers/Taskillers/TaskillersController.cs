using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskill.Domain;
using Taskill.Services.Auth;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
public class TaskillersController : ControllerBase
{
    private readonly SignInManager<Taskiller> _signInManager;
    private readonly IAuthService _authService;

    public TaskillersController(
        SignInManager<Taskiller> signInManager,
        IAuthService authService
    ) {
        _signInManager = signInManager;
        _authService = authService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateNewTaskiller([FromBody] TaskillerIn data)
    {
        await _authService.CreateNewTaskiller(data.email, data.password);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginIn data)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userName: data.email,
            password: data.password,
            isPersistent: false,
            lockoutOnFailure: true
        );

        if (result.Succeeded)
        {
            var tokens = await _authService.GenerateAccessToken(data.email);

            return Ok(tokens);
        }

        return BadRequest("Login failed.");
    }
}
