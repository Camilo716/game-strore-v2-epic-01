namespace GameStore.Core.Models;

public class Platform
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Type { get; set; }
}