using Asteradar.API.Models;

namespace Asteradar.API.Client;

public interface IAsteroidClient
{
    Task<List<Asteroid>> GetNearAsteroids(DateTime from, DateTime to);
}