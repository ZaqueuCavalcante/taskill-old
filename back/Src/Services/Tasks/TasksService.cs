using Microsoft.EntityFrameworkCore;
using Taskill.Controllers;
using Taskill.Database;

namespace Taskill.Services.Tasks;

public class TasksService : ITasksService
{
    private readonly TaskillDbContext _context;

    public TasksService(TaskillDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Task> CreateTask(uint userId, TaskIn data)
    {
        uint projectId;
        var projectExists = await _context.Projects.AnyAsync(p => p.UserId == userId && p.Id == data.projectId);
        if (projectExists)
        {
            projectId = data.projectId!.Value;
        }
        else
        {
            projectId = await _context.Projects
                .Where(p => p.UserId == userId && p.Name == "Today")
                .Select(p => p.Id).FirstAsync();
        }

        var task = new Domain.Task(userId, projectId, data.title, data.description, data.priority);

        _context.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }
}
