namespace GameStore.Api.Dtos.GenresDtos;

public class SimpleGenreDto
{
    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; }
}