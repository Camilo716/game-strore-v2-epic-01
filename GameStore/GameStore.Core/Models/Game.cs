namespace GameStore.Core.Models;

public class Game
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }

    public string Description { get; set; }

    public List<Genre> Genres { get; set; }

    public List<Platform> Platforms { get; set; }
}