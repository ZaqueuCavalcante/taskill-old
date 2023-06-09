namespace Taskill.Controllers;

public class TasksOut
{
    public uint? id { get; set; }

    public uint? projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public string status { get; set; }

    public string priority { get; set; }

    public DateTime creationDate { get; set; }

    public DateTime? dueDate { get; set; }

    public TasksOut() { }

    public TasksOut(Domain.Task task)
    {
        id = task.Id;
        projectId = task.ProjectId;
        title = task.Title;
        description = task.Description;
        status = task.Status.ToString();
        priority = task.Priority.ToString();
        creationDate = task.CreationDate;
        dueDate = task.DueDate;
    }
}
