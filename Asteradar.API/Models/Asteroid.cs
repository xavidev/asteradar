namespace Asteradar.API.Models;

public class Asteroid
{
    public DateOnly Date { get; init; }
    public double MinDiameter { get; init; }
    public double MaxDiameter { get; init; }
    public bool IsHazardous { get; init; }
    public string Name { get; init; }
    public string Planet { get; init; }
    public double Velocity { get; init; }
    public double Diameter => MinDiameter + MaxDiameter / 2;
}