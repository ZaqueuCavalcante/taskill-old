namespace Taskill.Controllers;

public class TaskIn
{
    public uint ProjectId { get; set; }

    public string Description { get; set; }

    public byte Priority { get; set; }
}

public class ProjectIn
{
    public string Name { get; set; }
}

public class LabelIn
{
    public string Name { get; set; }
}
