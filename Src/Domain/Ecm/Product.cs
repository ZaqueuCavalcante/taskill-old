namespace Taskill.Domain;

public class Product
{
    public uint Id { get; set; }

    public uint CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public uint Stock { get; set; }

    public byte Ratings { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Image> Images { get; set; }
}
