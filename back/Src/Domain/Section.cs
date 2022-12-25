namespace Taskill.Domain;

public class Section
{
    public uint Id { get; set; }

    public uint ProjectId { get; set; }

    public string Name { get; set; }

    public List<Task> Tasks { get; set; }
}
