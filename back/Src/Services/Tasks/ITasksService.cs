using Taskill.Controllers;

namespace Taskill.Services.Tasks;

public interface ITasksService
{
    Task<Domain.Task> CreateTask(uint userId, TaskIn data);

    Task CompleteTask(uint userId, uint taskId);
    Task UncompleteTask(uint userId, uint taskId);

    Task ChangeTaskPriority(uint userId, uint taskId, byte priority);

    Task ChangeTaskTitle(uint userId, uint taskId, string title);
    Task ChangeTaskDescription(uint userId, uint taskId, string description);

    Task<Domain.Task> GetTask(uint userId, uint taskId);

    Task<List<Domain.Task>> GetTasks(uint userId);
}
