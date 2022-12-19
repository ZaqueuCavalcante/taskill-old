using Taskill.Exceptions;
using Taskill.Extensions;

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
        SetName(name);
        CreationDate = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        if (name.IsEmpty() || name.Length < 3)
        {
            throw new DomainException("The label name should be contains more that 3 letters.");
        }

        Name = name;
    }
}
