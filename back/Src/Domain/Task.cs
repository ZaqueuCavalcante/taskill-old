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

    public DateTime? DueDate { get; set; }

    public List<Label> Labels { get; set; }

    public Task() { }

    public Task(
        uint userId,
        uint projectId,
        string title,
        string? description,
        byte priority
    ) {
        UserId = userId;
        ProjectId = projectId;
        Title = title;
        Description = description;
        Priority = priority;
        CreationDate = DateTime.UtcNow;
    }
}
