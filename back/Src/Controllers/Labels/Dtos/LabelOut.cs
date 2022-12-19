using Taskill.Domain;

namespace Taskill.Controllers;

public class LabelOut
{
    public uint id { get; set; }

    public string name { get; set; }

    public DateTime creationDate { get; set; }

    public LabelOut() {}

    public LabelOut(Label label)
    {
        id = label.Id;
        name = label.Name;
        creationDate = label.CreationDate;
    }
}
