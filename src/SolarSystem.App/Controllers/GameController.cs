using System.Numerics;
using Raylib_cs;

namespace SolarSystem.App.Controllers;

public sealed class GameController
{
    public float Scale { get; private set; } = 1e9f;
    public Vector2 CameraOffset { get; private set; } = Vector2.Zero;

    // --- Поля для слайдера ---
    private const float SliderMin = 0.1f;
    private const float SliderMax = 24.0f;
    private float _sliderValue = 1.0f;
    private Rectangle _sliderRect = new Rectangle(20, 100, 300, 20);
    private bool _isDragging = false;

    public double Dt => _isPaused ? 0.0 : _sliderValue * 3600.0;
    private bool _isPaused;

    public void Update()
    {
        float wheel = Raylib.GetMouseWheelMove();
        if (wheel != 0)
        {
            float oldScale = Scale;

            Scale -= wheel * 0.05f * Scale;
            if (Scale < 1e5f)
                Scale = 1e5f;
            if (Scale > 1e12f)
                Scale = 1e12f;

            float ratio = oldScale / Scale;
            Vector2 mousePos = Raylib.GetMousePosition();

            var screenCenter = new Vector2(
                Raylib.GetScreenWidth() / 2f,
                Raylib.GetScreenHeight() / 2f
            );

            CameraOffset = CameraOffset * ratio + (mousePos - screenCenter) * (1f - ratio);
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left) && !IsMouseOverSlider())
        {
            CameraOffset += Raylib.GetMouseDelta();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            _isPaused = !_isPaused;
        }

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        if (
            Raylib.IsMouseButtonPressed(MouseButton.Left)
            && Raylib.CheckCollisionPointRec(mousePos, _sliderRect)
        )
        {
            _isDragging = true;
        }

        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            float t = (mousePos.X - _sliderRect.X) / _sliderRect.Width;
            t = Math.Clamp(t, 0f, 1f);
            _sliderValue = SliderMin + t * (SliderMax - SliderMin);
        }
    }

    private bool IsMouseOverSlider()
    {
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), _sliderRect);
    }

    public string GetScaleText()
    {
        double km = Scale / 1000.0;
        return $"1 px = {km:N0} km";
    }

    public void DrawUI()
    {
        Raylib.DrawRectangleRec(_sliderRect, Color.DarkGray);

        float t = (_sliderValue - SliderMin) / (SliderMax - SliderMin);
        Rectangle filledRect = new(
            _sliderRect.X,
            _sliderRect.Y,
            _sliderRect.Width * t,
            _sliderRect.Height
        );
        Raylib.DrawRectangleRec(filledRect, Color.SkyBlue);

        float knobX = _sliderRect.X + _sliderRect.Width * t;
        float knobY = _sliderRect.Y + _sliderRect.Height / 2f;
        Raylib.DrawCircleV(new Vector2(knobX, knobY), 8, Color.White);

        string text = $"Speed: {_sliderValue:F1} h / frame";
        Raylib.DrawText(text, (int)_sliderRect.X, (int)_sliderRect.Y - 25, 18, Color.LightGray);
    }
}
