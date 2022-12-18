using Taskill.Controllers;

namespace Taskill.Services.Auth;

public interface IAuthService
{
    Task CreateTaskiller(string email, string password);

    Task<AccessTokenOut> Login(string email, string password);
}
