using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Extensions;
using Taskill.Services.Tasks;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class TasksController : ControllerBase
{
    private readonly TaskillDbContext _context;
    private readonly ITasksService _tasksService;

    public TasksController(
        TaskillDbContext context,
        ITasksService tasksService
    ) {
        _context = context;
        _tasksService = tasksService;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateTask([FromBody] TaskIn data)
    {
        var task = await _tasksService.CreateTask(User.Id(), data);

        return Ok(new TaskOut(task));
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllTasks()
    {
        var userId = User.Id();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId).ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("db"), AllowAnonymous]
    public async Task<IActionResult> SeedDb()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        return Ok();
    }
}
