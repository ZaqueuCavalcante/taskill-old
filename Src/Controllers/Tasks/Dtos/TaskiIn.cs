namespace Taskill.Controllers;

public class TaskIn
{
    /// <example>1</example>
    public uint? projectId { get; set; }

    /// <example>1</example>
    public uint? sectionId { get; set; }

    /// <example>Finish the project documentation</example>
    public string title { get; set; }

    /// <example>Look for all controllers and endpoints to make the features docs lalala.</example>
    public string? description { get; set; }

    /// <example>3</example>
    public byte priority { get; set; }
}
