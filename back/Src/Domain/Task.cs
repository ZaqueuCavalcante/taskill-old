using Taskill.Exceptions;
using Taskill.Extensions;

namespace Taskill.Domain;

public class Task
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public uint ProjectId { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public byte Priority { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int Index { get; set; }

    public List<Label> Labels { get; set; }

    public Task() { }

    public Task(
        uint userId,
        uint projectId,
        string title,
        string? description,
        byte priority,
        int position = 0
    ) {
        UserId = userId;
        SetProject(projectId);
        SetTitle(title);
        SetDescription(description);
        SetPriority(priority);
        SetIndex(position);
        CreationDate = DateTime.UtcNow;
    }

    public void SetTitle(string title)
    {
        if (title.IsEmpty() || title.Length < 3)
        {
            throw new DomainException("The task title should be contains more that 3 letters.");
        }

        Title = title;
    }

    public void SetPriority(byte priority)
    {
        if (priority > 3)
        {
            throw new DomainException("The task priority should be 0, 1, 2 or 3.");
        }

        Priority = priority;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetProject(uint projectId)
    {
        ProjectId = projectId;
    }

    public void SetLabels(List<Label> labels)
    {
        Labels = labels;
    }

    public void SetDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public void Complete()
    {
        CompletionDate = DateTime.Now;
    }

    public void Uncomplete()
    {
        CompletionDate = null;
    }
}
