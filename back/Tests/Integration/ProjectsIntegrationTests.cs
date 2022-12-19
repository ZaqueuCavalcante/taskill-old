using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Tests.Integration;

[TestFixture]
public class ProjectsIntegrationTests : ApiTestBase
{
    [Test]
    public async Task Deve_criar_um_novo_projeto()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn
        {
            name = "Taskill",
        };

        // Act
        var response = await _client.PostAsync("/projects", projectIn.ToStringContent());
        var project = await response.DeserializeTo<ProjectOut>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        project.name.Should().Be(projectIn.name);
    }

    [Test]
    public async Task Nao_deve_criar_um_novo_projeto_quando_ja_existir_um_com_o_mesmo_nome()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn
        {
            name = "Taskill",
        };

        // Act
        await _client.PostAsync("/projects", projectIn.ToStringContent());
        var response = await _client.PostAsync("/projects", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var projectsResponse = await _client.GetAsync("/projects");
        var projects = await projectsResponse.DeserializeTo<List<ProjectOut>>();

        projects.Count.Should().Be(2);
        projects[0].name.Should().Be(projectIn.name);
    }

    [Test]
    public async Task Nao_deve_renomear_um_projeto_quando_ja_existir_um_com_o_mesmo_nome()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn
        {
            name = "Taskill",
        };
        await _client.PostAsync("/projects", projectIn.ToStringContent());
        projectIn.name = "Today";

        // Act
        var response = await _client.PutAsync("/projects/2", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Already exists a project with this name.");
    }
}
