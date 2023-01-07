using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;
using static Taskill.Extensions.ProjectExtensions;
using System.IdentityModel.Tokens.Jwt;
using static Taskill.Configs.AuthorizationConfigs;
using Taskill.Settings;

namespace Taskill.Tests.Integration;

[TestFixture]
public class TaskillersIntegrationTests : ApiTestBase
{
    [Test]
    public async Task On_taskiller_creation__should_link_to_her_a_default_project()
    {
        // Arrange
        const string email = "taskiller@gmail.com";
        const string password = "Test@123";

        // Act
        await CreateTaskiller(email, password);
        await Login(email, password);

        // Assert
        var response = await _client.GetAsync("/projects");
        var projects = await response.DeserializeTo<List<ProjectOut>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        projects.Count.Should().Be(1);
        projects[0].name.Should().Be(DefaultProjectName);
    }

    [Test]
    public async Task On_taskiller_creation__should_throw_error_when_email_is_duplicated()
    {
        // Arrange
        const string email = "taskiller@gmail.com";
        const string password = "Test@123";
        await CreateTaskiller(email, password);

        var data = new TaskillerIn { email = email, password = password, };

        // Act
        var response = await _client.PostAsync("/taskillers", data.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Error on taskiller creation.");
    }

    [Test]
    public async Task A_taskiller_do_not_should_see_another_taskiller_tasks()
    {
        // Arrange
        const string joaoEmail = "joao@gmail.com";
        const string zeEmail = "ze@gmail.com";
        await CreateTaskiller(joaoEmail);
        await CreateTaskiller(zeEmail);

        await Login(joaoEmail);
        var joaoTaskId = await CreateTask("Joao task");

        await Login(zeEmail);

        // Act
        var response = await _client.GetAsync($"/tasks/{joaoTaskId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Task not found.");
    }

    [Test]
    public async Task Should_adds_taskiller_to_premium_plan__when_the_premium_token_is_correct()
    {
        // Arrange
        const string email = "taskiller@gmail.com";
        const string password = "Test@123";

        await CreateTaskiller(email, password);
        await Login(email, password);
        var authSettings = GetService<AuthSettings>();

        var data = new PremiumPlanIn { taskillerId = 1, token = authSettings.PremiumToken, };

        // Act
        var response = await _client.PutAsync("/taskillers/premium", data.ToStringContent());

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var jwt = await Login(email, password);

        // Assert
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);

        var premiumClaim = token.Claims.First(c => c.Type == PremiumClaim);

        premiumClaim.Value.Should().Be("true");
    }

    [Test]
    public async Task Should_not_adds_taskiller_to_premium_plan__when_the_premium_token_is_incorrect()
    {
        // Arrange
        const string email = "taskiller@gmail.com";
        const string password = "Test@123";

        await CreateTaskiller(email, password);
        await Login(email, password);

        var data = new PremiumPlanIn { taskillerId = 1, token = "incorrect_token_lalala", };

        // Act
        var response = await _client.PutAsync("/taskillers/premium", data.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.DeserializeTo<ErrorDto>();
        error.error.Should().Be("Invalid token.");
    }
}
