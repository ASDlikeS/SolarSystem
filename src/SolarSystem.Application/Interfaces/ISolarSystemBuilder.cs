using SolarSystem.Domain.Models;

namespace SolarSystem.Application.Interfaces;

public interface ISolarSystemBuilder
{
    List<CelestialBody> Build(IReadOnlyList<PlanetRow> planets);
}
