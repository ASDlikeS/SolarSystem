using SolarSystem.Domain.Math;

namespace SolarSystem.Domain.Models;

public abstract class CelestialBody : Entity
{
    public double MassKg { get; init; }
    public double RadiusM { get; init; }
    public Vector2D PositionM { get; set; }
    public Vector2D VelocityMs { get; set; }
    public string? TexturePath { get; set; }
    public double DiameterM => RadiusM * 2;
}
