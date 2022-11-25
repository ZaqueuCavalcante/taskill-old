namespace Taskill.Domain;

public class Project
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public List<Task> Tasks { get; set; }
}
