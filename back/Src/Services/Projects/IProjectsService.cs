using Taskill.Domain;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Services;

public interface IProjectsService
{
    Task<Project> CreateProject(uint userId, string name);
    Task RenameProject(uint userId, uint id, string name);

    Task ChangeProjectTaskIndex(uint userId, uint id, int oldIndex, int newIndex);

    Task<Project> GetProject(uint userId, uint id);
    Task<List<Project>> GetProjects(uint userId);
}
