using System.Diagnostics;
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

    public async Task<List<Asteroid>> GetNearAsteroids(DateTime @from, DateTime to)
    {
        var startDate = "2020-09-09";
        var endDate = "2020-09-16";
        // var startDate = DateOnly.FromDateTime(@from);
        // var endDate = DateOnly.FromDateTime(to);
        var uri =
            $"{this.client.BaseAddress}neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={this.configuration["API:Key"]}";

        var response = await this.client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadFromJsonAsync<Response.NearAsteroidsResponse>();
        return content?.NearEarthObjects.SelectMany(x => x.Value.Select(x => new Asteroid()
        {
            Date = DateOnly.Parse(x.CloseApproachData[0].CloseApproachDate),
            Name = x.Name,
            Planet = x.CloseApproachData[0].OrbitingBody,
            IsHazardous = x.IsPotentiallyHazardousAsteroid,
            Velocity = double.Parse(x.CloseApproachData[0].RelativeVelocity.KilometersPerHour),
            MaxDiameter = x.EstimatedDiameter.Kilometers.EstimatedDiameterMax,
            MinDiameter = x.EstimatedDiameter.Kilometers.EstimatedDiameterMin
        })).ToList();
    }
}