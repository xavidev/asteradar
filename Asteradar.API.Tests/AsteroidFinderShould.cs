using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asteradar.API.Client;
using Asteradar.API.Service;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using Xunit;

namespace Asteradar.API.Tests;

public class AsteroidFinderShould
{
    [Fact]
    public async Task Return_Asteroids_Of_Given_Planet()
    {
        var clientStub = new Mock<IAsteroidClient>();
        clientStub.Setup(x => x.GetNearAsteroids(It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
            .ReturnsAsync(CreateAsteroids());
        var sut = new AsteroidFinder(clientStub.Object);

        
        var result = await sut.GetNearAsteroids("earth");

        
        result.Count.Should().Be(2);
        result.First().Planet.Should().Be("earth");
    }

    private static List<AsteroidDTO> CreateAsteroids()
    {
        var asteroids = new List<AsteroidDTO>()
        {
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                Diameter = It.IsAny<double>(),
                IsHazardous = true,
                Name = It.IsAny<string>(),
                Planet = "earth",
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                Diameter = It.IsAny<double>(),
                IsHazardous = true,
                Name = It.IsAny<string>(),
                Planet = "earth",
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                Diameter = It.IsAny<double>(),
                IsHazardous = true,
                Name = It.IsAny<string>(),
                Planet = It.IsAny<string>(),
                Velocity = It.IsAny<double>()
            }
        };
        return asteroids;
    }
}