namespace Taskill.Controllers;

public class TasksOut
{
    public uint? id { get; set; }

    public uint? projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }

    public DateTime creationDate { get; set; }

    public DateTime? dueDate { get; set; }

    public TasksOut() { }

    public TasksOut(Domain.Task task)
    {
        id = task.Id;
        projectId = task.ProjectId;
        title = task.Title;
        description = task.Description;
        priority = task.Priority;
        creationDate = task.CreationDate;
        dueDate = task.DueDate;
    }
}
