using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Asteradar.API.Client;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Asteradar.API.Tests;

public class AsteradarAPITests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> fixture;

    public AsteradarAPITests(WebApplicationFactory<Program> fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public async Task  Test_Succes()
    {
        var client = this.fixture.CreateClient();
        
        var response = await client.GetAsync("/asteroids?planet=earth");

        var content = await response.Content.ReadFromJsonAsync<Response.NearAsteroidsResponse>();
        
        response.EnsureSuccessStatusCode();
        content.ElementCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Test_BadRequest()
    {
        var client = this.fixture.CreateClient();

        var response = await client.GetAsync("/asteroids");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}