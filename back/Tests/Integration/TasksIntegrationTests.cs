using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Taskill.Controllers;
using Task = System.Threading.Tasks.Task;

namespace Taskill.Tests.Integration;

[TestFixture]
public class TasksIntegrationTests : ApiTestBase
{
    [Test]
    public async Task Na_criacao_de_uma_task__quando_nao_for_informado_um_projeto__ela_deve_ser_vinculada_ao_default()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn
        {
            title = "Finish this project",
        };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        task.projectId.Should().Be(1);
    }

    [Test]
    public async Task Na_criacao_de_uma_task__quando_nao_for_informada_a_prioridade__ela_deve_ser_zero()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn
        {
            title = "Finish this project",
        };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var task = await response.DeserializeTo<TaskOut>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        task.priority.Should().Be(0);
    }

    [Test]
    public async Task Na_criacao_de_uma_task__quando_for_informada_uma_prioridade_invalida__deve_retornar_erro()
    {
        // Arrange
        await CreateTaskiller();
        await Login();

        var taskIn = new TaskIn
        {
            title = "Finish this project",
            priority = 5,
        };

        // Act
        var response = await _client.PostAsync("/tasks", taskIn.ToStringContent());
        var error = await response.DeserializeTo<ErrorDto>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.error.Should().Be("The task priority should be 0, 1, 2 or 3.");
    }
}
