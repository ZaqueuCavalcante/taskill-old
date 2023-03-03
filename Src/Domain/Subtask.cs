using Taskill.Exceptions;
using Taskill.Extensions;

namespace Taskill.Domain;

public class Subtask
{
    public uint Id { get; set; }

    public uint TaskId { get; set; }

    public string Title { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int Index { get; set; }

    public Subtask() { }

    public Subtask(
        uint taskId,
        string title,
        int index = 0
    ) {
        TaskId = taskId;
        SetTitle(title);
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

    public void SetIndex(int index)
    {
        Index = index;
    }
}
