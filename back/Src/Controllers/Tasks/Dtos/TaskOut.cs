namespace Taskill.Controllers;

public class TaskOut
{
    public uint id { get; set; }

    public uint projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }

    public DateTime? completionDate { get; set; }

    public TaskOut() { }

    public TaskOut(Domain.Task task)
    {
        id = task.Id;
        projectId = task.ProjectId;
        title = task.Title;
        description = task.Description;
        priority = task.Priority;
        completionDate = task.CompletionDate;
    }
}
