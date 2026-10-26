namespace SolarSystem.Domain.Math;

public readonly record struct Vector2D(double X, double Y)
{
    public static readonly Vector2D Zero = new(0, 0);

    public double LengthSquared => X * X + Y * Y;
    public double Length => System.Math.Sqrt(LengthSquared);

    public Vector2D Normalized()
    {
        var len = Length;
        return len < 1e-12 ? Zero : new(X / len, Y / len);
    }

    public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2D operator *(Vector2D a, double k) => new(a.X * k, a.Y * k);

    public static Vector2D operator *(double k, Vector2D a) => new(a.X * k, a.Y * k);

    public static Vector2D operator /(Vector2D a, double k) => new(a.X / k, a.Y / k);

    public static Vector2D operator -(Vector2D a) => new(-a.X, -a.Y);
}
