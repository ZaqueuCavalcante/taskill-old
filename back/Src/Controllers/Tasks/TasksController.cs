using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Extensions;
using Taskill.Services;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Controllers;

[ApiController, Route("[controller]")]
[Authorize(Roles = TaskillerRole)]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateTask([FromBody] TaskIn data)
    {
        var task = await _tasksService.CreateTask(User.Id(), data);

        return Ok(new TaskOut(task));
    }

    [HttpPost("sub")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateSubtask([FromBody] SubtaskIn data)
    {
        var task = await _tasksService.CreateSubtask(User.Id(), data);

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

    [HttpPut("{id}/reminder")]
    [Authorize(Roles = PremiumRole)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AddTaskReminder([FromRoute] uint id, [FromBody] TaskReminderIn data)
    {
        await _tasksService.AddTaskReminder(User.Id(), id, data.beforeInMinutes);

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

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(List<TasksOut>), 200)]
    public async Task<IActionResult> SearchTasks([FromRoute] string text)
    {
        var tasks = await _tasksService.SearchTasks(User.Id(), text);

        return Ok(tasks.ConvertAll(t => new TasksOut(t)));
    }
}
