namespace SolarSystem.Domain.Models;

public abstract class Entity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
