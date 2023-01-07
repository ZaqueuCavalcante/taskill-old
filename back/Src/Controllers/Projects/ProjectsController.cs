using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Extensions;
using Taskill.Services;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    [HttpPost("")]
    [ProducesResponseType(typeof(ProjectOut), 200)]
    public async Task<IActionResult> CreateProject([FromBody] ProjectIn data)
    {
        var project = await _projectsService.CreateProject(User.Id(), data.name);

        return Ok(new ProjectOut(project));
    }

    /// <summary>
    /// Rename a project.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RenameProject([FromRoute] uint id, [FromBody] ProjectIn data)
    {
        await _projectsService.RenameProject(User.Id(), id, data.name);

        return NoContent();
    }

    /// <summary>
    /// Creates a new project section.
    /// </summary>
    [HttpPost("{id}/sections")]
    [ProducesResponseType(typeof(ProjectSectionOut), 200)]
    public async Task<IActionResult> CreateProjectSection([FromRoute] uint id, [FromBody] ProjectSectionIn data)
    {
        var section = await _projectsService.CreateProjectSection(User.Id(), id, data.name);

        return Ok(new ProjectSectionOut(section));
    }

    /// <summary>
    /// Change the index of a task in a project.
    /// </summary>
    [HttpPut("{id}/tasks/{oldIndex}/move-to/{newIndex}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeProjectTaskIndex([FromRoute] uint id, [FromRoute] int oldIndex, [FromRoute] int newIndex)
    {
        await _projectsService.ChangeProjectTaskIndex(User.Id(), id, oldIndex, newIndex);

        return NoContent();
    }

    /// <summary>
    /// Change the index of a task in a project section.
    /// </summary>
    [HttpPut("{id}/sections/{sectionId}/tasks/{oldIndex}/move-to/{newIndex}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeProjectSectionTaskIndex([FromRoute] uint id, [FromRoute] uint sectionId, [FromRoute] int oldIndex, [FromRoute] int newIndex)
    {
        await _projectsService.ChangeProjectSectionTaskIndex(User.Id(), id, sectionId, oldIndex, newIndex);

        return NoContent();
    }

    /// <summary>
    /// Gets a project.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProjectOut), 200)]
    public async Task<IActionResult> GetProject([FromRoute] uint id)
    {
        var project = await _projectsService.GetProject(User.Id(), id);

        return Ok(new ProjectOut(project));
    }

    /// <summary>
    /// Gets many projects.
    /// </summary>
    [HttpGet("")]
    [ProducesResponseType(typeof(List<ProjectOut>), 200)]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectsService.GetProjects(User.Id());

        return Ok(projects.ConvertAll(p => new ProjectOut(p)));
    }
}
