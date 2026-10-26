using Dapper;
using Npgsql;
using SolarSystem.Data.Abstractions;
using SolarSystem.Domain.Models;

namespace SolarSystem.Data;

public class PlanetRepository : IPlanetRepository
{
    private readonly string _connString;

    public PlanetRepository(string connString) => _connString = connString;

    public async Task<IReadOnlyList<PlanetRow>> GetAllAsync(CancellationToken cl = default)
    {
        await using var conn = new NpgsqlConnection(_connString);
        var rows = await conn.QueryAsync<PlanetRow>(
            "SELECT id, name, mass_kg, radius_m, orbit_radius_m, orbital_speed_ms, color_hex, parent_id, texture_path FROM planets ORDER BY parent_id NULLS FIRST, orbit_radius_m"
        );

        return rows.AsList();
    }
}
