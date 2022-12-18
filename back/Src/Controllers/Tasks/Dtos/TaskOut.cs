namespace Taskill.Controllers;

public class TaskOut
{
    public uint? projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }

    public TaskOut() { }

    public TaskOut(Domain.Task task)
    {
        projectId = task.ProjectId;
        title = task.Title;
        description = task.Description;
        priority = task.Priority;
    }
}
