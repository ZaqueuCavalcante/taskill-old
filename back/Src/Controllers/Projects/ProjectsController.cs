using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Extensions;
using static Taskill.Configurations.AuthorizationConfigurations;

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
    public async Task<IActionResult> CreateNewProject([FromBody] ProjectIn projectIn)
    {
        var userId = User.Id();

        var project = new Project(userId, projectIn.name);

        _context.Add(project);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProjects()
    {
        var userId = User.Id();

        var projects = await _context.Projects
            .Where(t => t.UserId == userId).ToListAsync();

        return Ok(projects);
    }
}
