using Asteradar.API.Client;
using Asteradar.API.Models;

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

        return result.Where(x => x.IsHazardous &&
                                 !string.IsNullOrEmpty(x.Planet) &&
                                 x.Planet.Equals(planet,
                                     StringComparison.OrdinalIgnoreCase)
            )
            .OrderByDescending(x => x.Diameter)
            .Select(x => new AsteroidDTO()
            {
                Date = x.Date,
                Diameter = x.Diameter,
                Name = x.Name,
                Planet = x.Planet,
                Velocity = x.Velocity,
                IsHazardous = x.IsHazardous
            })
            .ToList();
    }
}