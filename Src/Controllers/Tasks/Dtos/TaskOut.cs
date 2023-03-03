namespace Taskill.Controllers;

public class TaskOut
{
    public uint id { get; set; }

    public uint projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }

    public DateTime creationDate { get; set; }

    public DateTime? dueDate { get; set; }

    public DateTime? completionDate { get; set; }

    public int index { get; set; }

    public List<TaskLabelOut> labels { get; set; }

    public List<SubtaskOut> subtasks { get; set; }

    public TaskOut() { }

    public TaskOut(Domain.Task task)
    {
        id = task.Id;
        projectId = task.ProjectId;
        title = task.Title;
        description = task.Description;
        priority = task.Priority;
        creationDate = task.CreationDate;
        dueDate = task.DueDate;
        completionDate = task.CompletionDate;
        index = task.Index;
        labels = task.Labels?.ConvertAll(l => new TaskLabelOut(l)) ?? new();
        subtasks = task.Subtasks?.ConvertAll(s => new SubtaskOut(s)) ?? new();
    }
}
