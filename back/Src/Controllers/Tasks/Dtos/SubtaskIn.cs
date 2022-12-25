namespace Taskill.Controllers;

public class SubtaskIn
{
    public uint parentTaskId { get; set; }

    public string title { get; set; }

    public string? description { get; set; }

    public byte priority { get; set; }
}
