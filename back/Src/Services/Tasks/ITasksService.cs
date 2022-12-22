using Taskill.Controllers;

namespace Taskill.Services;

public interface ITasksService
{
    Task<Domain.Task> CreateTask(uint userId, TaskIn data);

    Task CompleteTask(uint userId, uint taskId);
    Task UncompleteTask(uint userId, uint taskId);

    Task ChangeTaskPriority(uint userId, uint taskId, byte priority);

    Task ChangeTaskTitle(uint userId, uint taskId, string title);
    Task ChangeTaskDescription(uint userId, uint taskId, string description);

    Task ChangeTaskProject(uint userId, uint taskId, uint projectId);

    Task ChangeTaskLabels(uint userId, uint taskId, List<uint> labels);

    Task ChangeTaskDueDate(uint userId, uint taskId, DateTime? dueDate);

    Task<Domain.Task> GetTask(uint userId, uint taskId);

    Task<List<Domain.Task>> GetTasks(uint userId);

    Task<List<Domain.Task>> SearchTasks(uint userId, string text);
}
