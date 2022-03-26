using System.Threading.Tasks;
using Asteradar.API.Service;
using Xunit;

namespace Asteradar.API.Tests;

public class AsteroidFinderShould
{
    [Fact]
    public async Task Return_Top3_Potentially_Risk_Near_Asteroids()
    {
        var sut = new AsteroidFinder();

        sut.GetNearAsteroids();
    }
}