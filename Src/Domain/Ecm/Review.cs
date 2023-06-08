namespace Taskill.Domain;

public class Review
{
    public uint Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}
