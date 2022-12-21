using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Database;
using Taskill.Extensions;
using Taskill.Services.Tasks;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class TasksController : ControllerBase
{
    private readonly TaskillDbContext _context;
    private readonly ITasksService _tasksService;

    public TasksController(
        TaskillDbContext context,
        ITasksService tasksService
    ) {
        _context = context;
        _tasksService = tasksService;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateTask([FromBody] TaskIn data)
    {
        var task = await _tasksService.CreateTask(User.Id(), data);

        return Ok(new TaskOut(task));
    }

    [HttpPut("{id}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteTask([FromRoute] uint id)
    {
        await _tasksService.CompleteTask(User.Id(), id);

        return NoContent();
    }

    [HttpPut("{id}/uncomplete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UncompleteTask([FromRoute] uint id)
    {
        await _tasksService.UncompleteTask(User.Id(), id);

        return NoContent();
    }

    [HttpPut("{id}/priority/{newPriority}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskPriority([FromRoute] uint id, [FromRoute] byte newPriority)
    {
        await _tasksService.ChangeTaskPriority(User.Id(), id, newPriority);

        return NoContent();
    }

    [HttpPut("{id}/title")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskTitle([FromRoute] uint id, [FromBody] TaskTitleIn data)
    {
        await _tasksService.ChangeTaskTitle(User.Id(), id, data.title);

        return NoContent();
    }

    [HttpPut("{id}/description")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskDescription([FromRoute] uint id, [FromBody] TaskDescriptionIn data)
    {
        await _tasksService.ChangeTaskDescription(User.Id(), id, data.description);

        return NoContent();
    }

    [HttpPut("{id}/project/{newProjectId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskProject([FromRoute] uint id, [FromRoute] uint newProjectId)
    {
        await _tasksService.ChangeTaskProject(User.Id(), id, newProjectId);

        return NoContent();
    }

    [HttpPut("{id}/labels")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskLabels([FromRoute] uint id, [FromBody] TaskLabelsIn data)
    {
        await _tasksService.ChangeTaskLabels(User.Id(), id, data.labels);

        return NoContent();
    }

    [HttpPut("{id}/due-date")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskDueDate([FromRoute] uint id, [FromBody] TaskDueDateIn data)
    {
        await _tasksService.ChangeTaskDueDate(User.Id(), id, data.dueDate);

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> GetTask([FromRoute] uint id)
    {
        var task = await _tasksService.GetTask(User.Id(), id);

        return Ok(new TaskOut(task));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(List<TasksOut>), 200)]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _tasksService.GetTasks(User.Id());

        return Ok(tasks.ConvertAll(t => new TasksOut(t)));
    }









    [HttpGet("db"), AllowAnonymous]
    public async Task<IActionResult> SeedDb()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        return Ok();
    }
}
