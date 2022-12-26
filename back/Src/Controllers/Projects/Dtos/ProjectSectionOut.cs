using Taskill.Domain;

namespace Taskill.Controllers;

public class ProjectSectionOut
{
    public uint id { get; set; }

    public string name { get; set; }

    public ProjectSectionOut() {}

    public ProjectSectionOut(Section section)
    {
        id = section.Id;
        name = section.Name;
    }
}
