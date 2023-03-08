using Microsoft.EntityFrameworkCore;
using Taskill.Controllers;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Exceptions;
using Task = System.Threading.Tasks.Task;
using static Taskill.Extensions.ProjectExtensions;

namespace Taskill.Services;

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
                .Where(p => p.UserId == userId && p.Name == DefaultProjectName)
                .Select(p => p.Id).FirstAsync();
        }

        var sectionExists = await _context.Sections.AnyAsync(s => s.ProjectId == projectId && s.Id == data.sectionId);
        var sectionId = sectionExists ? data.sectionId : null;

        var taskIndex = sectionExists ?
            await _context.Sections.CountAsync(s => s.Id == sectionId) : await _context.Tasks.CountAsync(t => t.ProjectId == projectId);

        var task = new Domain.Task(userId, projectId, sectionId, data.title, data.description, data.priority, taskIndex);

        _context.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<Subtask> CreateSubtask(uint userId, SubtaskIn data)
    {
        var task = await _context.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == data.taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        var subtaskIndex = await _context.Subtasks.CountAsync(s => s.TaskId == data.taskId);

        var subtask = new Subtask(data.taskId, data.title, subtaskIndex);

        _context.Subtasks.Add(subtask);
        await _context.SaveChangesAsync();

        return subtask;
    }

    public async Task CompleteTask(uint userId, uint taskId)
    {
        var task = await _context.Tasks
            .Include(t => t.Actions)
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.Complete();

        await _context.SaveChangesAsync();
    }

    public async Task UncompleteTask(uint userId, uint taskId)
    {
        var task = await _context.Tasks
            .Include(t => t.Actions)
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.Uncomplete();

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskPriority(uint userId, uint taskId, byte priority)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.SetPriority(priority);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskTitle(uint userId, uint taskId, string title)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.SetTitle(title);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskDescription(uint userId, uint taskId, string description)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.SetDescription(description);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskProject(uint userId, uint taskId, uint projectId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        var project = await _context.Projects.AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == projectId);
        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        task.SetProject(projectId);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskLabels(uint userId, uint taskId, List<uint> labels)
    {
        var task = await _context.Tasks
            .Include(t => t.Labels)
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        var userLabels = await _context.Labels.AsNoTracking()
            .Where(l => l.UserId == userId && labels.Contains(l.Id)).ToListAsync();

        task.SetLabels(userLabels);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeTaskDueDate(uint userId, uint taskId, DateTime? dueDate)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.SetDueDate(dueDate);

        await _context.SaveChangesAsync();
    }

    public async Task<Domain.Task> GetTask(uint userId, uint taskId)
    {
        var task = await _context.Tasks.AsNoTracking()
            .Include(t => t.Labels)
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

        if (task == null)
        {
            throw new DomainException("Task not found.", 404);
        }

        task.Subtasks = await _context.Subtasks.AsNoTracking()
            .Where(s => s.TaskId == taskId)
            .ToListAsync();

        return task;
    }

    public async Task<List<Domain.Task>> GetTasks(uint userId)
    {
        return await _context.Tasks.AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreationDate)
            .ToListAsync();
    }

    public async Task<List<Domain.Task>> SearchTasks(uint userId, string text)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Where(t =>
                t.UserId == userId &&
                EF.Functions.ToTsVector("portuguese", t.Title + " " + t.Description).Matches(text)
            )
            .OrderByDescending(t => t.CreationDate)
            .ToListAsync();
    }
}
