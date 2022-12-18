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
}

public static class Extensions
{
    public static StringContent ToStringContent(this object obj)
    {
        var serializedObject = JsonConvert.SerializeObject(obj);
        return new StringContent(serializedObject, Encoding.UTF8, "application/json");
    }

    public static async Task<T> DeserializeTo<T>(this HttpResponseMessage httpResponse)
    {
        var responseAsString = await httpResponse.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseAsString);
    }
}

public class ErrorDto { public string error { get; set; } = ""; }
