using Taskill.Domain;

namespace Taskill.Controllers;

public class TaskLabelOut
{
    public uint id { get; set; }

    public string name { get; set; }

    public TaskLabelOut(Label label)
    {
        id = label.Id;
        name = label.Name;
    }
}
