using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asteradar.API.Client;
using Asteradar.API.Models;
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
    public async Task Return_Top_3_Dangerous_Asteroids_Of_Given_Planet_Order_By_Size()
    {
        var clientStub = new Mock<IAsteroidClient>();
        clientStub.Setup(x => x.GetNearAsteroids(It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
            .ReturnsAsync(CreateAsteroids());
        var sut = new AsteroidFinder(clientStub.Object);

        
        var result = await sut.GetHazardousAsteroids("earth");

        
        result.Count.Should().Be(3);
        result[0].Name.Should().Be("First");
        result[1].Name.Should().Be("Second");
        result[2].Name.Should().Be("Third");
    }

    [Fact]
    public async Task Return_Existing_Asteroids_When_No_Top3()
    {
        var clientStub = new Mock<IAsteroidClient>();
        var asteroids = CreateTwoHazardousAsteroids();
        clientStub.Setup(x => x.GetNearAsteroids(It.IsAny<DateTime>(),
                It.IsAny<DateTime>()))
            .ReturnsAsync(asteroids);
        var sut = new AsteroidFinder(clientStub.Object);
        
        
        var result = await sut.GetHazardousAsteroids("earth");

        
        result.Count.Should().Be(2);
    }

    private static List<Asteroid> CreateTwoHazardousAsteroids()
    {
        var asteroids = new List<Asteroid>()
        {
            new AsteroidBuilder(It.IsAny<string>(), "earth", true).Build(),
            new AsteroidBuilder(It.IsAny<string>(), "earth", true).Build(),
        };
        
        return asteroids;
    }

    [Fact]
    public async Task Throw_Error_When_Invalid_Planet()
    {
        var clientStub = new Mock<IAsteroidClient>();
        var sut = new AsteroidFinder(clientStub.Object);

        var act = async () => await sut.GetHazardousAsteroids(" ");
        
        await act.Should().ThrowExactlyAsync<ArgumentException>();
    }

    private static List<Asteroid> CreateAsteroids()
    {
        var asteroids = new List<Asteroid>()
        {
            new AsteroidBuilder("Third", "earth", true).Small().Build(),
            new AsteroidBuilder("Second", "earth", true).Medium().Build(),
            new AsteroidBuilder("First", "earth", true).Big().Build(),
            new AsteroidBuilder(It.IsAny<string>(), "random", true).Build(),
            new AsteroidBuilder(It.IsAny<string>(), "Earth", true).ExtraSmall().Build()
        };
        
        return asteroids;
    }
}