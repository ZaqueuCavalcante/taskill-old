using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;
using static Taskill.Extensions.ProjectExtensions;

namespace Taskill.Tests.Integration;

[TestFixture]
public class TaskillersIntegrationTests : ApiTestBase
{
    [Test]
    public async Task On_taskiller_creation__should_link_a_default_project()
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

        error.error.Should().Be("Erro na criação do usuário.");
    }
}
