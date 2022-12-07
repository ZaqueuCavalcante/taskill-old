using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Extensions;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class TasksController : ControllerBase
{
    private readonly TaskillDbContext _context;

    public TasksController(TaskillDbContext context)
    {
        _context = context;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateNewTask([FromBody] TaskIn taskIn)
    {
        var userId = User.Id();

        var projectExists = await _context.Projects.AnyAsync(p => p.UserId == userId && p.Id == taskIn.projectId);
        if (!projectExists)
        {
            throw new Exception("Erro lalala");
        }

        var task = new Domain.Task(userId, taskIn.projectId, taskIn.title, taskIn.description, taskIn.priority);

        _context.Add(task);
        await _context.SaveChangesAsync();

        return NoContent();
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
