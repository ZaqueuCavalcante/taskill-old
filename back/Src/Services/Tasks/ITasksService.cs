using Taskill.Controllers;

namespace Taskill.Services.Tasks;

public interface ITasksService
{
    Task<Domain.Task> CreateTask(uint userId, TaskIn data);
}
