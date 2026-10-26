using SolarSystem.Application.Interfaces;
using SolarSystem.Domain.Math;
using SolarSystem.Domain.Models;

namespace SolarSystem.Application;

public sealed class SolarSystemBuilder : ISolarSystemBuilder
{
    public List<CelestialBody> Build(IReadOnlyList<PlanetRow> planets)
    {
        List<CelestialBody> celestialBodies = [];
        foreach (var planet in planets)
        {
            var pos = new Vector2D(planet.OrbitRadiusM, 0);
            var vel = new Vector2D(0, planet.OrbitalSpeedMs);

            if (planet.ParentId != null)
            {
                var parent = celestialBodies.First(p => p.Id == planet.ParentId);

                pos = parent.PositionM + pos;
                vel = parent.VelocityMs + vel;
            }
            celestialBodies.Add(
                new Planet
                {
                    Id = planet.Id,
                    Name = planet.Name,
                    MassKg = planet.MassKg,
                    RadiusM = planet.RadiusM,
                    VelocityMs = vel,
                    OrbitRadiusM = planet.OrbitRadiusM,
                    PositionM = pos,
                    ColorHex = planet.ColorHex,
                    ParentId = planet.ParentId,
                    TexturePath = planet.TexturePath,
                }
            );
        }

        celestialBodies.Add(
            new Star
            {
                Id = 0,
                Name = "Sun",
                MassKg = 1.98847e30,
                RadiusM = 6.957e8,
                PositionM = new Vector2D(),
                VelocityMs = new Vector2D(),
                LuminosityW = 3.828e28,
                TexturePath = "sun.png",
            }
        );

        return celestialBodies;
    }
}
