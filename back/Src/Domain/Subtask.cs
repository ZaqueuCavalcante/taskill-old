namespace Taskill.Domain;

public class Subtask : Task
{
    public uint? ParentTaskId { get; set; }

    public Subtask() {}

    public Subtask(
        uint userId,
        uint projectId,
        string title,
        string? description,
        byte priority,
        uint parentTaskId,
        int position = 0
    ) : base(userId, projectId, title, description, priority, position)
    {
        ParentTaskId = parentTaskId;
    }
}
