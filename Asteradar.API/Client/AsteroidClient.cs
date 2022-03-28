using Asteradar.API.Models;

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

    public async Task<List<Asteroid>> GetNearAsteroids(DateTime @from, DateTime to)
    {
        var startDate = DateOnly.FromDateTime(@from).ToString("yyyy-MM-dd");
        var endDate = DateOnly.FromDateTime(to).ToString("yyyy-MM-dd");
        var uri =
            $"{this.client.BaseAddress}neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={this.configuration["API:Key"]}";

        var response = await this.client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<Response.NearAsteroidsResponse>();
        
        return content.NearEarthObjects.SelectMany(x => x.Value.Select(asteroid => new Asteroid()
        {
            Date = DateOnly.Parse(asteroid.CloseApproachData[0].CloseApproachDate),
            Name = asteroid.Name,
            Planet = asteroid.CloseApproachData[0].OrbitingBody,
            IsHazardous = asteroid.IsPotentiallyHazardousAsteroid,
            Velocity = double.Parse(asteroid.CloseApproachData[0].RelativeVelocity.KilometersPerHour),
            MaxDiameter = asteroid.EstimatedDiameter.Kilometers.EstimatedDiameterMax,
            MinDiameter = asteroid.EstimatedDiameter.Kilometers.EstimatedDiameterMin
        })).ToList();
    }
}