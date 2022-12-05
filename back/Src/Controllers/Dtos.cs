namespace Taskill.Controllers;

public class TaskIn
{
    public uint projectId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }
}

public class ProjectIn
{
    public string name { get; set; }
}

public class LabelIn
{
    public string name { get; set; }
}
