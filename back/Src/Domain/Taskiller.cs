using Microsoft.AspNetCore.Identity;

namespace Taskill.Domain;

public class Taskiller : IdentityUser<uint>
{
    public List<Project> Projects { get; set; }
    public List<Task> Tasks { get; set; }
    public List<Label> Labels { get; set; }

    public Taskiller(string email)
    {
        UserName = email;
        Email = email;
    }
}
