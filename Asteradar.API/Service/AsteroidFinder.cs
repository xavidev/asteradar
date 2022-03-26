using Asteradar.API.Client;

namespace Asteradar.API.Service;

public class AsteroidFinder
{
    private readonly IAsteroidClient client;

    public AsteroidFinder(IAsteroidClient client)
    {
        this.client = client;
    }

    public async Task<IReadOnlyList<AsteroidDTO>> GetNearAsteroids(string planet)
    {
        var now = DateTime.Now;
        var result = await this.client.GetNearAsteroids(now, now.AddDays(7));

        return result.Where(x => x.IsHazardous && x.Planet.Equals(planet, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}