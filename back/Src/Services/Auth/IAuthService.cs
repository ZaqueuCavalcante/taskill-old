using Taskill.Controllers;

namespace Taskill.Services;

public interface IAuthService
{
    Task CreateTaskiller(string email, string password);

    Task<AccessTokenOut> Login(string email, string password);
}
