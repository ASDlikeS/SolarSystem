using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarSystem.App;
using SolarSystem.Application.Interfaces;
using SolarSystem.Data.Abstractions;
using SolarSystem.Rendering.Interfaces;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = Host.CreateApplicationBuilder(
    new HostApplicationBuilderSettings { Args = args, ContentRootPath = AppContext.BaseDirectory }
);

DI.Init(builder);

using var host = builder.Build();
var repo = host.Services.GetRequiredService<IPlanetRepository>();
var solarBuilder = host.Services.GetRequiredService<ISolarSystemBuilder>();

var planets = await repo.GetAllAsync();
var bodies = solarBuilder.Build(planets);

var renderer = host.Services.GetRequiredService<IRender>();
var game = new Game(bodies, renderer);
game.Run();
