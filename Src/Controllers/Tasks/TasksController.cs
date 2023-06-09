using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskill.Extensions;
using Taskill.Domain;
using Taskill.Services;

namespace Taskill.Controllers;

[Authorize]
[ApiController, Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService) =>
        _tasksService = tasksService;

    /// <summary>
    /// Creates a new task.
    /// </summary>
    [HttpPost("")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateTask([FromBody] TaskIn data)
    {
        var task = await _tasksService.CreateTask(User.Id(), data);

        return Ok(new TaskOut(task));
    }

    /// <summary>
    /// Creates a new sub-task.
    /// </summary>
    [HttpPost("sub")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> CreateSubtask([FromBody] SubtaskIn data)
    {
        var task = await _tasksService.CreateSubtask(User.Id(), data);

        return Ok(new SubtaskOut(task));
    }

    /// <summary>
    /// Completes a task.
    /// </summary>
    [HttpPut("{id}/complete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> CompleteTask([FromRoute] uint id)
    {
        await _tasksService.CompleteTask(User.Id(), id);

        return NoContent();
    }

    /// <summary>
    /// Uncompletes a task.
    /// </summary>
    [HttpPut("{id}/uncomplete")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UncompleteTask([FromRoute] uint id)
    {
        await _tasksService.UncompleteTask(User.Id(), id);

        return NoContent();
    }

    /// <summary>
    /// Changes a task priority.
    /// </summary>
    [HttpPut("{id}/priority/{newPriority}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskPriority([FromRoute] uint id, [FromRoute] Priority newPriority)
    {
        await _tasksService.ChangeTaskPriority(User.Id(), id, newPriority);

        return NoContent();
    }

    /// <summary>
    /// Changes a task title.
    /// </summary>
    [HttpPut("{id}/title")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskTitle([FromRoute] uint id, [FromBody] TaskTitleIn data)
    {
        await _tasksService.ChangeTaskTitle(User.Id(), id, data.title);

        return NoContent();
    }

    /// <summary>
    /// Changes a task description.
    /// </summary>
    [HttpPut("{id}/description")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskDescription([FromRoute] uint id, [FromBody] TaskDescriptionIn data)
    {
        await _tasksService.ChangeTaskDescription(User.Id(), id, data.description);

        return NoContent();
    }

    /// <summary>
    /// Changes a task project.
    /// </summary>
    [HttpPut("{id}/project/{newProjectId}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskProject([FromRoute] uint id, [FromRoute] uint newProjectId)
    {
        await _tasksService.ChangeTaskProject(User.Id(), id, newProjectId);

        return NoContent();
    }

    /// <summary>
    /// Changes a task labels.
    /// </summary>
    [HttpPut("{id}/labels")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskLabels([FromRoute] uint id, [FromBody] TaskLabelsIn data)
    {
        await _tasksService.ChangeTaskLabels(User.Id(), id, data.labels);

        return NoContent();
    }

    /// <summary>
    /// Changes a task due date.
    /// </summary>
    [HttpPut("{id}/due-date")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> ChangeTaskDueDate([FromRoute] uint id, [FromBody] TaskDueDateIn data)
    {
        await _tasksService.ChangeTaskDueDate(User.Id(), id, data.dueDate);

        return NoContent();
    }

    /// <summary>
    /// Gets a task.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskOut), 200)]
    public async Task<IActionResult> GetTask([FromRoute] uint id)
    {
        var task = await _tasksService.GetTask(User.Id(), id);

        return Ok(new TaskOut(task));
    }

    /// <summary>
    /// Gets many tasks.
    /// </summary>
    [HttpGet("")]
    [ProducesResponseType(typeof(List<TasksOut>), 200)]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _tasksService.GetTasks(User.Id());

        return Ok(tasks.ConvertAll(t => new TasksOut(t)));
    }

    /// <summary>
    /// Search for a task by title or description.
    /// </summary>
    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(List<TasksOut>), 200)]
    public async Task<IActionResult> SearchTasks([FromRoute] string text)
    {
        var tasks = await _tasksService.SearchTasks(User.Id(), text);

        return Ok(tasks.ConvertAll(t => new TasksOut(t)));
    }
}
