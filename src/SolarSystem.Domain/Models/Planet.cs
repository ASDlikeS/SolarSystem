namespace SolarSystem.Domain.Models;

public sealed class Planet : CelestialBody
{
    public double OrbitRadiusM { get; init; }
    public string ColorHex { get; init; } = "#FFFFFF";
    public int? ParentId { get; init; }
}
