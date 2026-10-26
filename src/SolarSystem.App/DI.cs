using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarSystem.Application;
using SolarSystem.Application.Interfaces;
using SolarSystem.Data;
using SolarSystem.Data.Abstractions;
using SolarSystem.Rendering;
using SolarSystem.Rendering.Interfaces;

namespace SolarSystem.App;

public static class DI
{
    public static void Init(HostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPlanetRepository>(sp =>
        {
            var cfg = sp.GetRequiredService<IConfiguration>();
            var conn =
                cfg.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException(
                    "There's no connection string in appsettings.json"
                );
            return new PlanetRepository(conn);
        });

        builder.Services.AddSingleton<ISolarSystemBuilder, SolarSystemBuilder>();
        builder.Services.AddSingleton<IRender, Render>();
    }
}
