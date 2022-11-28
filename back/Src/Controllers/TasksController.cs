using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;

namespace Taskill.Controllers;

[ApiController]
[Route("[controller]")]
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
        var task = new Domain.Task(1, taskIn.ProjectId, taskIn.Description, taskIn.Priority);

        _context.Add(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _context.Tasks.ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("db")]
    public async Task<IActionResult> SeedDb()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        return Ok();
    }
}
