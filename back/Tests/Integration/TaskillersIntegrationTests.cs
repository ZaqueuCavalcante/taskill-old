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
    public async Task Na_criacao_de_um_taskiller__um_projeto_default_deve_ser_vinculado_a_ele()
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
    public async Task Na_criacao_de_um_taskiller__deve_retornar_erro_caso_o_email_ja_pertenca_a_outro_usuario()
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
