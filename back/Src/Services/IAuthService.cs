using Taskill.Controllers;

namespace Taskill.Services.Auth;

public interface IAuthService
{
    Task CreateNewTaskiller(string email, string password);

    Task<AccessTokenOut> GenerateAccessToken(string email);
}
