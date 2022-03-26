using Asteradar.API.Service;

namespace Asteradar.API.Client;

public interface IAsteroidClient
{
    Task<List<AsteroidDTO>> GetNearAsteroids(DateTime from, DateTime to);
}