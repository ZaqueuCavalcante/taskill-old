using Taskill.Exceptions;
using Taskill.Extensions;
using static Taskill.Extensions.ProjectExtensions;

namespace Taskill.Domain;

public class Project
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public string Name { get; set; }

    public Layout Layout { get; set; }

    public DateTime CreationDate { get; set; }

    public List<Section> Sections { get; set; }

    public List<Task> Tasks { get; set; }

    public Project(uint userId, string name)
    {
        UserId = userId;
        SetName(name);
        CreationDate = DateTime.UtcNow;
    }

    public void SetName(string name)
    {
        if (name.IsEmpty() || name.Length < 3)
        {
            throw new DomainException("The project name should be contains more that 3 letters.");
        }

        if (Name == DefaultProjectName)
        {
            throw new DomainException("The default project name is immutable.");
        }

        Name = name;
    }
}
