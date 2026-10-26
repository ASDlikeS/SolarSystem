namespace SolarSystem.Domain.Models;

public sealed class PlanetRow
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public double MassKg { get; init; }
    public double RadiusM { get; init; }
    public double OrbitRadiusM { get; init; }
    public double OrbitalSpeedMs { get; init; }
    public string ColorHex { get; init; } = string.Empty;
    public string? TexturePath { get; init; }
    public int? ParentId { get; init; }
}
