using Microsoft.EntityFrameworkCore;
using Taskill.Database;
using Taskill.Domain;
using Taskill.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Services.Labels;

public class LabelsService : ILabelsService
{
    private readonly TaskillDbContext _context;

    public LabelsService(TaskillDbContext context)
    {
        _context = context;
    }

    public async Task<Label> CreateLabel(uint userId, string name)
    {
        var label = await _context.Labels.FirstOrDefaultAsync(l => l.UserId == userId && l.Name == name);
        if (label != null)
        {
            return label;
        }

        label = new Label(userId, name);

        _context.Add(label);

        await _context.SaveChangesAsync();

        return label;
    }

    public async Task RenameLabel(uint userId, uint id, string name)
    {
        var label = await _context.Labels.FirstOrDefaultAsync(l => l.UserId == userId && l.Id == id);
        if (label == null)
        {
            throw new DomainException("Label not found.", 404);
        }

        var projectWithThisName = await _context.Labels.FirstOrDefaultAsync(l => l.UserId == userId && l.Name == name);
        if (projectWithThisName != null)
        {
            throw new DomainException("Already exists a label with this name.");
        }

        label.SetName(name);

        await _context.SaveChangesAsync();
    }

    public async Task<Label> GetLabel(uint userId, uint id)
    {
        var label = await _context.Labels.FirstOrDefaultAsync(l => l.UserId == userId && l.Id == id);

        if (label == null)
        {
            throw new DomainException("Label not found.", 404);
        }

        return label;
    }

    public async Task<List<Label>> GetLabels(uint userId)
    {
        return await _context.Labels
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.CreationDate)
            .ToListAsync();
    }
}
