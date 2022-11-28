using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;

namespace Taskill.Controllers;

[ApiController]
[Route("[controller]")]
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
        var project = new Project(1, projectIn.Name);

        _context.Add(project);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _context.Projects.ToListAsync();

        return Ok(projects);
    }
}
