using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Extensions;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class ProjectsController : ControllerBase
{
    private readonly TaskillDbContext _context;

    public ProjectsController(TaskillDbContext context)
    {
        _context = context;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProject([FromBody] ProjectIn data)
    {
        var userId = User.Id();

        var projectAlreadyExists = await _context.Labels
            .AnyAsync(l => l.UserId == userId && l.Name == data.name);
        if (projectAlreadyExists)
        {
            return NoContent();
        }

        var project = new Project(userId, data.name);

        _context.Add(project);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<ProjectOut>), 200)]
    public async Task<IActionResult> GetAllProjects()
    {
        var userId = User.Id();

        var projects = await _context.Projects
            .Where(t => t.UserId == userId).ToListAsync();

        return Ok(projects.ConvertAll(p => new ProjectOut(p)));
    }
}
