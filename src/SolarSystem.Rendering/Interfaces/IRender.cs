using System.Numerics;
using SolarSystem.Domain.Models;

namespace SolarSystem.Rendering.Interfaces;

public interface IRender
{
    void Draw(List<CelestialBody> bodies, double scale, Vector2 cameraOffset);
    void Dispose();
}
