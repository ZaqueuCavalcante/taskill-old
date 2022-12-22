using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Extensions;
using Taskill.Services;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(ProjectOut), 200)]
    public async Task<IActionResult> CreateProject([FromBody] ProjectIn data)
    {
        var project = await _projectsService.CreateProject(User.Id(), data.name);

        return Ok(new ProjectOut(project));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RenameProject([FromRoute] uint id, [FromBody] ProjectIn data)
    {
        await _projectsService.RenameProject(User.Id(), id, data.name);

        return NoContent();
    }

    [HttpPut("{id}/tasks/{oldIndex}/move-to/{newIndex}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeProjectTaskIndex([FromRoute] uint id, [FromRoute] int oldIndex, [FromRoute] int newIndex)
    {
        await _projectsService.ChangeProjectTaskIndex(User.Id(), id, oldIndex, newIndex);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectOut), 200)]
    public async Task<IActionResult> GetProject([FromRoute] uint id)
    {
        var project = await _projectsService.GetProject(User.Id(), id);

        return Ok(new ProjectOut(project));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<ProjectOut>), 200)]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectsService.GetProjects(User.Id());

        return Ok(projects.ConvertAll(p => new ProjectOut(p)));
    }
}
