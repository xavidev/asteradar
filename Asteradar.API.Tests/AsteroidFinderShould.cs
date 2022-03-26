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
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                MinDiameter = 111.123,
                MaxDiameter = 211.123,
                IsHazardous = true,
                Name = "Third",
                Planet = "earth",
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                MinDiameter = 123.123,
                MaxDiameter = 222.123,
                IsHazardous = true,
                Name = "Second",
                Planet = "earth",
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                MinDiameter = 123.123,
                MaxDiameter = 222.124,
                IsHazardous = true,
                Name = "First",
                Planet = "earth",
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                MinDiameter = 123.123,
                MaxDiameter = 123.123,
                IsHazardous = true,
                Name = It.IsAny<string>(),
                Planet = It.IsAny<string>(),
                Velocity = It.IsAny<double>()
            },
            new()
            {
                Date = DateOnly.FromDateTime(26.March(2022)),
                MinDiameter = 123.123,
                MaxDiameter = 123.123,
                IsHazardous = true,
                Name = It.IsAny<string>(),
                Planet = "Earth",
                Velocity = It.IsAny<double>()
            }
        };
        return asteroids;
    }
}