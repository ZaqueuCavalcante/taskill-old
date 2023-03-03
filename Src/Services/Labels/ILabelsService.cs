using Taskill.Domain;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Services;

public interface ILabelsService
{
    Task<Label> CreateLabel(uint userId, string name);
    Task RenameLabel(uint userId, uint id, string name);

    Task<Label> GetLabel(uint userId, uint id);
    Task<List<Label>> GetLabels(uint userId);
}
