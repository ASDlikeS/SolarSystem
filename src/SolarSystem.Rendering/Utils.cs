using Raylib_cs;

namespace SolarSystem.Rendering;

internal static class Utils
{
    internal static Color GetColor(string colorHEX)
    {
        if (colorHEX.Length != 7)
            throw new NotSupportedException("GET COLOR EXCEPTION: color format not supported.");

        string colorNoHash = colorHEX.Replace("#", "");
        byte red = Convert.ToByte(colorNoHash.Substring(0, 2), 16);
        byte green = Convert.ToByte(colorNoHash.Substring(2, 2), 16);
        byte blue = Convert.ToByte(colorNoHash.Substring(4, 2), 16);

        return new Color(red, green, blue, (byte)255);
    }
}
