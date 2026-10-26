using SolarSystem.Domain.Models;

namespace SolarSystem.Data.Abstractions;

public interface IPlanetRepository
{
    Task<IReadOnlyList<PlanetRow>> GetAllAsync(CancellationToken cl = default);
}
