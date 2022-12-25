namespace Taskill.Domain;

public class Subtask : Task
{
    public uint? ParentTaskId { get; set; }
}
