using Taskill.Domain;

namespace Taskill.Controllers;

public class ProjectOut
{
    public uint id { get; set; }

    public string name { get; set; }

    public DateTime creationDate { get; set; }

    public List<TaskOut> tasks { get; set; }

    public ProjectOut() { }

    public ProjectOut(Project project)
    {
        id = project.Id;
        name = project.Name;
        creationDate = project.CreationDate;
        tasks = project.Tasks?.ConvertAll(t => new TaskOut(t)) ?? new();
    }
}
