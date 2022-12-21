using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly TaskillDbContext _context;

    public ProjectsService(TaskillDbContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateProject(uint userId, string name)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.UserId == userId && p.Name == name);
        if (project != null)
        {
            return project;
        }

        project = new Project(userId, name);

        _context.Add(project);

        await _context.SaveChangesAsync();

        return project;
    }

    public async Task RenameProject(uint userId, uint id, string name)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);
        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        var projectWithThisName = await _context.Projects.FirstOrDefaultAsync(p => p.UserId == userId && p.Name == name);
        if (projectWithThisName != null)
        {
            throw new DomainException("Already exists a project with this name.");
        }

        project.SetName(name);

        await _context.SaveChangesAsync();
    }

    public async Task<Project> GetProject(uint userId, uint id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);

        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        project.Tasks = await _context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId && t.ProjectId == id)
            .OrderByDescending(t => t.CreationDate)
            .ToListAsync();

        return project;
    }

    public async Task<List<Project>> GetProjects(uint userId)
    {
        return await _context.Projects
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreationDate)
            .ToListAsync();
    }
}
