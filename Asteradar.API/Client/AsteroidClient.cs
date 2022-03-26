using Asteradar.API.Models;
using Asteradar.API.Service;

namespace Asteradar.API.Client;

public class AsteroidClient : IAsteroidClient
{
    private readonly HttpClient client;
    private readonly IConfiguration configuration;

    public AsteroidClient(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        this.configuration = configuration;
    }

    public Task<List<Asteroid>> GetNearAsteroids(DateTime @from, DateTime to)
    {
        throw new NotImplementedException();
    }
}