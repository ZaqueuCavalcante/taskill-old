using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Extensions;
using Taskill.Services;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class LabelsController : ControllerBase
{
    private readonly ILabelsService _labelsService;

    public LabelsController(ILabelsService labelsService)
    {
        _labelsService = labelsService;
    }

    /// <summary>
    /// Creates a new label.
    /// </summary>
    [HttpPost("")]
    [ProducesResponseType(typeof(LabelOut), 200)]
    public async Task<IActionResult> CreateLabel([FromBody] LabelIn data)
    {
        var label = await _labelsService.CreateLabel(User.Id(), data.name);

        return Ok(new LabelOut(label));
    }

    /// <summary>
    /// Rename a label.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RenameLabel([FromRoute] uint id, [FromBody] LabelIn data)
    {
        await _labelsService.RenameLabel(User.Id(), id, data.name);

        return NoContent();
    }

    /// <summary>
    /// Gets a label.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectOut), 200)]
    public async Task<IActionResult> GetLabel([FromRoute] uint id)
    {
        var label = await _labelsService.GetLabel(User.Id(), id);

        return Ok(new LabelOut(label));
    }

    /// <summary>
    /// Gets many labels.
    /// </summary>
    [HttpGet("")]
    [ProducesResponseType(typeof(List<LabelOut>), 200)]
    public async Task<IActionResult> GetLabels()
    {
        var labels = await _labelsService.GetLabels(User.Id());

        return Ok(labels.ConvertAll(l => new LabelOut(l)));
    }
}
