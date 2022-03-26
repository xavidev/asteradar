namespace Asteradar.API.Service;

public class AsteroidDTO
{
    public string Name { get; set; }
    public double Diameter { get; set; }
    public double Velocity { get; set; }
    public DateOnly Date { get; set; }
    public string Planet { get; set; }
    public bool IsHazardous { get; set; }
}