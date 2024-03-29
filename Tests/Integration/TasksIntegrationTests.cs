using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Domain;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Tests.Integration;

[TestFixture]
public class TasksIntegrationTests : ApiTestBase
{
    [Test]
    public async Task On_task_creation__when_only_the_title_is_defined__the_provided_title_should_be_used()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.title.Should().Be(taskIn.title);
    }

    [Test]
    public async Task On_task_creation__when_only_the_title_is_defined__the_default_project_should_be_used()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.projectId.Should().Be(1);
    }

    [Test]
    public async Task On_task_creation__when_only_the_title_is_defined__the_priority_should_be_0()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.priority.Should().Be(nameof(Priority.High));
    }

    [Test]
    public async Task On_task_creation__when_only_the_title_is_defined__the_description_should_be_null()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.description.Should().BeNull();
    }

    [Test]
    public async Task On_task_creation__when_only_the_title_is_defined__the_due_date_should_be_null()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.dueDate.Should().BeNull();
    }

    [Test]
    public async Task On_task_creation__when_the_priority_is_not_defined__it_should_be_0()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn { title = "Finish this project" };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        task.priority.Should().Be(nameof(Priority.High));
    }

    [Test]
    public async Task Should_complete_task()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        var taskId = await CreateTask();

        // Act
        var response = await _client.PutAsync($"/tasks/{taskId}/complete", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var task = await GetTask(taskId);

        task.completionDate.Should().NotBeNull();
    }

    [Test]
    public async Task Should_uncomplete_task()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        var taskId = await CreateTask();
        await _client.PutAsync($"/tasks/complete/{taskId}", null);

        // Act
        var response = await _client.PutAsync($"/tasks/{taskId}/uncomplete", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var task = await GetTask(taskId);

        task.completionDate.Should().BeNull();
    }

    [Test]
    public async Task Should_change_task_priority()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        var taskId = await CreateTask();
        const Priority newPriority = Priority.High;

        // Act
        var response = await _client.PutAsync($"/tasks/{taskId}/priority/{newPriority}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var task = await GetTask(taskId);

        task.priority.Should().Be(nameof(Priority.High));
    }

    [Test]
    public async Task Should_change_task_title()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        var taskId = await CreateTask();
        var data = new TaskTitleIn { title = "New title to test endpoint lalala..." };

        // Act
        var response = await _client.PutAsync($"/tasks/{taskId}/title", data.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var task = await GetTask(taskId);

        task.title.Should().Be(data.title);
    }

    [Test]
    public async Task Should_change_task_description()
    {
        // Arrange
        await CreateTaskiller();
        await Login();
        var taskId = await CreateTask();
        var data = new TaskDescriptionIn { description = "New description to test endpoint lalala..." };

        // Act
        var response = await _client.PutAsync($"/tasks/{taskId}/description", data.ToStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var task = await GetTask(taskId);

        task.description.Should().Be(data.description);
    }
}
