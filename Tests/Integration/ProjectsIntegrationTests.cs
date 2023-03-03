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
    public async Task Should_create_a_new_project()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn { name = "Taskill" };

        // Act
        var response = await _client.PostAsync("/projects", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var project = await response.DeserializeTo<ProjectOut>();
        project.name.Should().Be(projectIn.name);
    }

    [Test]
    public async Task Not_should_create_a_duplicated_project()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn { name = "Taskill" };

        // Act
        await _client.PostAsync("/projects", projectIn.ToStringContent());
        var response = await _client.PostAsync("/projects", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Project already exists.");
    }

    [Test]
    public async Task Should_rename_a_project()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn { name = "Taskill" };
        await _client.PostAsync("/projects", projectIn.ToStringContent());
        projectIn.name = "Taskill FrontEnd";

        // Act
        var response = await _client.PutAsync("/projects/2", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task Should_adds_a_new_section_to_project()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        await CreateProject("Taskill");

        var data = new ProjectSectionIn { name = "Todo" };

        // Act
        var response = await _client.PostAsync("/projects/2/sections", data.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var section = await response.DeserializeTo<ProjectSectionOut>();
        section.name.Should().Be(data.name);
    }

    [Test]
    public async Task Not_should_rename_a_project__when_already_exists_another_with_same_name()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn { name = "Taskill" };
        await _client.PostAsync("/projects", projectIn.ToStringContent());
        projectIn.name = DefaultProjectName;

        // Act
        var response = await _client.PutAsync("/projects/2", projectIn.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.DeserializeTo<ErrorDto>();

        error.error.Should().Be("Project already exists.");
    }

    [Test]
    public async Task Should_get_a_specific_project()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var projectIn = new ProjectIn { name = "Taskill" };
        await _client.PostAsync("/projects", projectIn.ToStringContent());

        // Act
        var response = await _client.GetAsync("/projects/2");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var project = await response.DeserializeTo<ProjectOut>();
        project.name.Should().Be(projectIn.name);
    }

    [Test]
    public async Task Should_get_all_user_projects()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        await CreateProject("Taskill");
        await CreateProject("DotNet");
        await CreateProject("React");

        // Act
        var response = await _client.GetAsync("/projects");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var projects = await response.DeserializeTo<List<ProjectOut>>();
        projects.Count.Should().Be(4);
    }

    [Test]
    public async Task Inside_project__should_change_task_index_from_1_to_3()
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

    [Test]
    public async Task Inside_project_section__should_change_task_index_from_1_to_3()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var data = new ProjectSectionIn { name = "Todo" };
        await _client.PostAsync("/projects/1/sections", data.ToStringContent());

        await CreateTask("First", 1, 1);
        await CreateTask("Second", 1, 1);
        await CreateTask("Third", 1, 1);
        await CreateTask("Four", 1, 1);

        const int oldIndex = 0;
        const int newIndex = 2;

        // Act
        var response = await _client.PutAsync($"/projects/1/sections/1/tasks/{oldIndex}/move-to/{newIndex}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var project = await GetProject(1);

        project.tasks[0].title.Should().Be("Second");
        project.tasks[1].title.Should().Be("Third");
        project.tasks[2].title.Should().Be("First");
        project.tasks[3].title.Should().Be("Four");
    }
}
