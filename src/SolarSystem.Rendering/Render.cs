using System.Numerics;
using Raylib_cs;
using SolarSystem.Domain.Models;
using SolarSystem.Rendering.Interfaces;

namespace SolarSystem.Rendering;

public sealed class Render : IRender, IDisposable
{
    private readonly TextureManager _textureManager;
    private Texture2D _backgroundTexture;
    private bool _isBackgroundLoaded;

    public Render()
    {
        _textureManager = new TextureManager();
    }

    public void Draw(List<CelestialBody> bodies, double scale, Vector2 cameraOffset)
    {
        if (!_isBackgroundLoaded)
        {
            _backgroundTexture = _textureManager.GetTexture("stars.png");
            _isBackgroundLoaded = true;
        }
        float centerX = Raylib.GetScreenWidth() / 2f + cameraOffset.X;
        float centerY = Raylib.GetScreenHeight() / 2f + cameraOffset.Y;

        if (_backgroundTexture.Width > 0)
        {
            float bgOffsetX = cameraOffset.X * 0.1f;
            float bgOffsetY = cameraOffset.Y * 0.1f;

            var sourceRec = new Rectangle(
                0,
                0,
                _backgroundTexture.Width,
                _backgroundTexture.Height
            );

            var destRec = new Rectangle(
                bgOffsetX - 100,
                bgOffsetY - 100,
                Raylib.GetScreenWidth() + 200,
                Raylib.GetScreenHeight() + 200
            );

            Raylib.DrawTexturePro(
                _backgroundTexture,
                sourceRec,
                destRec,
                new Vector2(0, 0),
                0f,
                Color.White
            );
        }

        foreach (var body in bodies)
        {
            float screenX = centerX + (float)(body.PositionM.X / scale);
            float screenY = centerY - (float)(body.PositionM.Y / scale);

            float radiusPx = (float)(body.RadiusM / scale);
            if (radiusPx < 2f)
                radiusPx = 2f;

            Texture2D texture = _textureManager.GetTexture(body.TexturePath);

            if (texture.Width > 0)
            {
                float sizePx = radiusPx * 2;
                var sourceRec = new Rectangle(0, 0, texture.Width, texture.Height);

                var destRec = new Rectangle(screenX - radiusPx, screenY - radiusPx, sizePx, sizePx);
                var origin = new Vector2(sizePx / 2f, sizePx / 2f);

                Raylib.DrawTexturePro(texture, sourceRec, destRec, origin, 0f, Color.White);
            }
            else
            {
                Color color = body is Planet p ? Utils.GetColor(p.ColorHex) : Color.Gold;
                Raylib.DrawCircle((int)screenX, (int)screenY, radiusPx, color);
            }
        }
    }

    public void Dispose()
    {
        _textureManager.UnloadAll();
    }
}
