using Raylib_cs;
using SolarSystem.App.Controllers;
using SolarSystem.Application.Physics;
using SolarSystem.Domain.Models;
using SolarSystem.Rendering.Interfaces;

namespace SolarSystem.App;

public class Game
{
    private readonly List<CelestialBody> _bodies;
    private readonly IRender _renderer;
    private readonly GameController _controller;

    public Game(List<CelestialBody> bodies, IRender renderer)
    {
        _bodies = bodies;
        _renderer = renderer;
        _controller = new GameController();
    }

    public void Run()
    {
        Raylib.InitWindow(1920, 1080, "Solar System");
        Raylib.SetTargetFPS(120);

        while (!Raylib.WindowShouldClose())
        {
            _controller.Update();

            NewtonGravity.Step(_bodies, _controller.Dt);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            _renderer.Draw(_bodies, _controller.Scale, _controller.CameraOffset);

            Raylib.DrawText(_controller.GetScaleText(), 10, 10, 20, Color.White);
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 40, 20, Color.Green);
            Raylib.DrawText("STOP TIME: Space", 10, 120, 20, Color.White);
            _controller.DrawUI();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
        _renderer.Dispose();
    }
}
