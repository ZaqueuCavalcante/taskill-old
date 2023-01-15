using Taskill.Exceptions;
using Taskill.Extensions;

namespace Taskill.Domain;

public class Subtask
{
    public uint Id { get; set; }

    public uint TaskId { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int Index { get; set; }

    public Subtask() { }

    public Subtask(
        uint taskId,
        string title,
        string? description,
        int index = 0
    ) {
        TaskId = taskId;
        SetTitle(title);
        SetDescription(description);
        SetIndex(index);
        CreationDate = DateTime.UtcNow;
    }

    public void SetTitle(string title)
    {
        if (title.IsEmpty() || title.Length < 3)
        {
            throw new DomainException("The subtask title should be contains more that 3 letters.");
        }

        Title = title;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}
