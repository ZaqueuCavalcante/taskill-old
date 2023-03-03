using Taskill.Domain;

namespace Taskill.Controllers;

public class SubtaskOut
{
    public uint id { get; set; }

    public string title { get; set; }

    public DateTime creationDate { get; set; }

    public DateTime? completionDate { get; set; }

    public int index { get; set; }

    public SubtaskOut() { }

    public SubtaskOut(Subtask subtask)
    {
        id = subtask.Id;
        title = subtask.Title;
        creationDate = subtask.CreationDate;
        completionDate = subtask.CompletionDate;
        index = subtask.Index;
    }
}
