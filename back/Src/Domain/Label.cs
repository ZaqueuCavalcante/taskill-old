namespace Taskill.Domain;

public class Label
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Name { get; set; }

    public DateTime CreationDate { get; set; }

    public List<Task> Tasks { get; set; }

    public Label(uint userId, string name)
    {
        UserId = userId;
        Name = name;
        CreationDate = DateTime.UtcNow;
    }
}
