using SolarSystem.Domain.Math;
using SolarSystem.Domain.Models;

namespace SolarSystem.Application.Physics;

public sealed class NewtonGravity
{
    public static void Step(List<CelestialBody> celestialBodies, double dt)
    {
        for (int i = 0; i < celestialBodies.Count; i++)
        {
            var aSum = new Vector2D();
            for (int j = 0; j < celestialBodies.Count; j++)
            {
                if (i == j)
                    continue;

                Vector2D distance = celestialBodies[j].PositionM - celestialBodies[i].PositionM;
                double R2 = distance.LengthSquared;

                if (R2 < 1e-9)
                    continue;

                double a = Constants.G * celestialBodies[j].MassKg / R2; // a = G * M / R^2 (2nd Newton's Law)

                aSum += distance.Normalized() * a;
            }

            celestialBodies[i].VelocityMs += aSum * dt;
        }

        foreach (var el in celestialBodies)
            el.PositionM += el.VelocityMs * dt;
    }
}
