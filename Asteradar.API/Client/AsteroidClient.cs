namespace Asteradar.API.Client;

public class AsteroidClient
{
    private readonly HttpClient client;
    private readonly IConfiguration configuration;

    public AsteroidClient(HttpClient client, IConfiguration configuration)
    {
        this.client = client;
        this.configuration = configuration;
    }
}