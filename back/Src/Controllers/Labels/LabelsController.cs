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
public class LabelsController : ControllerBase
{
    private readonly TaskillDbContext _context;

    public LabelsController(TaskillDbContext context)
    {
        _context = context;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateNewLabel([FromBody] LabelIn labelIn)
    {
        var userId = User.Id();

        var label = new Label(userId, labelIn.name);

        _context.Add(label);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllLabels()
    {
        var userId = User.Id();

        var labels = await _context.Labels
            .Where(l => l.UserId == userId).ToListAsync();

        return Ok(labels);
    }
}
