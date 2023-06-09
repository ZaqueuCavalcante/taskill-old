using Taskill.Exceptions;
using Taskill.Extensions;

namespace Taskill.Domain;

public class Task
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public uint ProjectId { get; set; }

    public uint? SectionId { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public Status Status { get; set; }

    public Priority Priority { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime? DueDate { get; set; }

    public int Index { get; set; }

    public List<Label> Labels { get; set; }

    public List<Subtask> Subtasks { get; set; }

    public Task() { }

    public Task(
        uint userId,
        uint projectId,
        uint? sectionId,
        string title,
        string? description,
        Priority priority,
        int index = 0
    ) {
        UserId = userId;
        SetProject(projectId);
        SectionId = sectionId;
        SetTitle(title);
        SetDescription(description);
        Priority = priority;
        SetIndex(index);
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
