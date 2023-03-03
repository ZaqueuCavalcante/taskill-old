using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Exceptions;
using Taskill.Extensions;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Services;

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
            throw new DomainException("Project already exists.");
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
            throw new DomainException("Project already exists.");
        }

        project.SetName(name);

        await _context.SaveChangesAsync();
    }

    public async Task<Section> CreateProjectSection(uint userId, uint id, string name)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);
        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        var section = new Section(id, name);

        _context.Add(section);

        await _context.SaveChangesAsync();

        return section;
    }

    public async Task ChangeProjectTaskIndex(uint userId, uint id, int oldIndex, int newIndex)
    {
        var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userId && p.Id == id);
        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && t.ProjectId == id)
            .OrderBy(t => t.Index)
            .ToListAsync();

        tasks.MoveTask(oldIndex, newIndex);

        await _context.SaveChangesAsync();
    }

    public async Task ChangeProjectSectionTaskIndex(uint userId, uint projectId, uint sectionId, int oldIndex, int newIndex)
    {
        var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userId && p.Id == projectId);
        if (project == null)
        {
            throw new DomainException("Project not found.", 404);
        }

        var section = await _context.Sections.AsNoTracking().FirstOrDefaultAsync(s => s.ProjectId == projectId && s.Id == sectionId);
        if (section == null)
        {
            throw new DomainException("Section not found.", 404);
        }

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && t.SectionId == sectionId)
            .OrderBy(t => t.Index)
            .ToListAsync();

        tasks.MoveTask(oldIndex, newIndex);

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
            .OrderBy(t => t.Index)
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
