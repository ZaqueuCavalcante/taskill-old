namespace Taskill.Domain;

public class Section
{
    public uint Id { get; set; }

    public uint ProjectId { get; set; }

    public string Name { get; set; }

    public List<Task> Tasks { get; set; }

    public Section(uint projectId, string name)
    {
        ProjectId = projectId;
        Name = name;
    }
}
