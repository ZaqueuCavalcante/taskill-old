namespace Taskill.Controllers;

public class TaskIn
{
    public uint? projectId { get; set; }

    public uint? sectionId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }
}
