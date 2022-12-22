using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Taskill.Controllers;
using Taskill.Database;
using Taskill.Settings;

namespace Taskill.Tests.Integration;

public class ApiTestBase
{
    protected TaskillWebApplicationFactory _factory = null!;
    protected TaskillDbContext _context = null!;
    protected HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new TaskillWebApplicationFactory();
    }

    [SetUp]
    public void SetupBeforeEachTest()
    {
        using var scope = _factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<TaskillDbContext>();
        _client = _factory.CreateClient();

        var cnn = _context.Database.GetConnectionString()!;

        if (Env.IsTesting() && cnn.Contains("Database=taskill-tests-db"))
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }

    protected async Task CreateTaskiller(
        string email = "task@killer.com",
        string password = "rE9gt@4erg"
    ) {
        var data = new TaskillerIn { email = email, password = password, };

        var response = await _client.PostAsync("/taskillers", data.ToStringContent());

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    protected async Task Login(
        string email = "task@killer.com",
        string password = "rE9gt@4erg"
    ) {
        var data = new LoginIn { email = email, password = password, };

        var response = await _client.PostAsync("/taskillers/login", data.ToStringContent());

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokens = await response.DeserializeTo<AccessTokenOut>();

        tokens.expires_in.Should().Be(60);
        tokens.token_type.Should().Be("Bearer");
        tokens.access_token.Should().NotBeNullOrWhiteSpace();

        _client.DefaultRequestHeaders.Remove("Authorization");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokens.access_token}");
    }

    protected async Task<uint> CreateTask(string title = "Finish this project")
    {
        var data = new TaskIn
        {
            title = title,
        };

        var response = await _client.PostAsync("/tasks", data.ToStringContent());
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var task = await response.DeserializeTo<TaskOut>();

        return task.id;
    }

    protected async Task<TaskOut> GetTask(uint id)
    {
        var response = await _client.GetAsync($"/tasks/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        return await response.DeserializeTo<TaskOut>();
    }

    protected async Task<ProjectOut> GetProject(uint id)
    {
        var response = await _client.GetAsync($"/projects/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        return await response.DeserializeTo<ProjectOut>();
    }
}
