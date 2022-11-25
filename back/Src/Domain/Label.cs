namespace Taskill.Domain;

public class Label
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public List<Task> Tasks { get; set; }
}
