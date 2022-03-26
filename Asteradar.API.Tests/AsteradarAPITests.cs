using System.Threading.Tasks;
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
    public async Task  Test_Api()
    {
        var client = this.fixture.CreateClient();
        var response = await client.GetAsync("/asteroids?planet=earth");

        response.EnsureSuccessStatusCode();
    }
}