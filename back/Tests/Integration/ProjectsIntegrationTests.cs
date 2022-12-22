using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;
using static Taskill.Extensions.ProjectExtensions;

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
        projectIn.name = DefaultProjectName;

        // Act
        var response = await _client.PutAsync("/projects/2", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Already exists a project with this name.");
    }

    [Test]
    public async Task Deve_mudar_uma_task_da_primeira_posicao_para_a_terceira()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        await CreateTask("First");
        await CreateTask("Second");
        await CreateTask("Third");
        await CreateTask("Four");

        const int oldIndex = 0;
        const int newIndex = 2;

        // Act
        var response = await _client.PutAsync($"/projects/1/tasks/{oldIndex}/move-to/{newIndex}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var project = await GetProject(1);

        project.tasks[0].title.Should().Be("Second");
        project.tasks[1].title.Should().Be("Third");
        project.tasks[2].title.Should().Be("First");
        project.tasks[3].title.Should().Be("Four");
    }
}
