using Raylib_cs;

namespace SolarSystem.Rendering;

public sealed class TextureManager
{
    private readonly Dictionary<string, Texture2D> _textures = [];

    public Texture2D GetTexture(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return default;
        }

        string fullPath = Path.Combine(AppContext.BaseDirectory, "Textures", path);

        var doesExist = _textures.TryGetValue(fullPath, out Texture2D texture);
        if (doesExist)
            return texture;

        var loadedTexture = Raylib.LoadTexture(fullPath);
        if (loadedTexture.Id == 0)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось загрузить текстуру: {fullPath}");
            return default;
        }
        Raylib.SetTextureFilter(loadedTexture, TextureFilter.Bilinear);
        _textures.Add(fullPath, loadedTexture);
        return loadedTexture;
    }

    public void UnloadAll()
    {
        foreach (var texture in _textures.Values)
        {
            Raylib.UnloadTexture(texture);
        }

        _textures.Clear();
    }
}
