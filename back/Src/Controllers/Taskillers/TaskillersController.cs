using Microsoft.AspNetCore.Mvc;
using Taskill.Services;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
public class TaskillersController : ControllerBase
{
    private readonly IAuthService _authService;

    public TaskillersController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CreateTaskiller([FromBody] TaskillerIn data)
    {
        await _authService.CreateTaskiller(data.email, data.password);

        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AccessTokenOut), 200)]
    public async Task<IActionResult> Login([FromBody] LoginIn data)
    {
        var tokens = await _authService.Login(data.email, data.password);

        return Ok(tokens);
    }
}
