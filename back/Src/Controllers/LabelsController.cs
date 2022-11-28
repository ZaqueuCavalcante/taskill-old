using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;

namespace Taskill.Controllers;

[ApiController]
[Route("[controller]")]
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
        var label = new Label(1, labelIn.Name);

        _context.Add(label);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllLabels()
    {
        var labels = await _context.Labels.ToListAsync();

        return Ok(labels);
    }
}
