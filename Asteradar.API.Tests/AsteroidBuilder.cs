using System;
using System.Numerics;
using Asteradar.API.Models;
using FluentAssertions.Extensions;
using Moq;

namespace Asteradar.API.Tests;

public class AsteroidBuilder
{
    private const double MIN_DIAMETER = 1.000;
    private const double SMALL_DIAMETER = 5.000;
    private const double MEDIUM_DIAMETER = 10.000;
    private const double BIG_DIAMETER = 20.000;

    private DateOnly date;
    private double minDiameter;
    private double maxDiameter;
    private bool isHazardous;
    private string name;
    private string planet;
    private double velocity;

    public AsteroidBuilder(string name, string planet, bool isHazardous)
    {
        this.minDiameter = MIN_DIAMETER;
        this.maxDiameter = MIN_DIAMETER;
        this.date = It.IsAny<DateOnly>();
        this.name = name;
        this.planet = planet;
        this.isHazardous = isHazardous;
        this.velocity = It.IsAny<double>();
    }

    public AsteroidBuilder Big()
    {
        this.minDiameter = BIG_DIAMETER;
        this.maxDiameter = BIG_DIAMETER;

        return this;
    }

    public AsteroidBuilder Medium()
    {
        this.minDiameter = MEDIUM_DIAMETER;
        this.maxDiameter = MEDIUM_DIAMETER;

        return this;
    }

    public AsteroidBuilder Small()
    {
        this.minDiameter = SMALL_DIAMETER;
        this.maxDiameter = SMALL_DIAMETER;

        return this;
    }
    
    public AsteroidBuilder ExtraSmall()
    {
        this.minDiameter = MIN_DIAMETER;
        this.maxDiameter = MIN_DIAMETER;

        return this;
    }

    public Asteroid Build()
    {
        return new Asteroid()
        {
            Date = this.date,
            Name = this.name,
            Planet = this.planet,
            Velocity = this.velocity,
            IsHazardous = this.isHazardous,
            MaxDiameter = this.maxDiameter,
            MinDiameter = this.minDiameter
        };
    }
}