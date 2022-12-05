using Microsoft.AspNetCore.Identity;

namespace Taskill.Domain;

public class Taskiller : IdentityUser<uint>
{
    public Taskiller(string email)
    {
        UserName = email;
        Email = email;
    }
}
