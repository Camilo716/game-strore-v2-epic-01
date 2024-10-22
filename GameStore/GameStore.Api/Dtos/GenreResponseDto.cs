using GameStore.Core.Models;

namespace GameStore.Api.Dtos;

public class GenreResponseDto(Genre genre)
{
    private readonly Genre _genre = genre;

    public Guid Id => _genre.Id;

    public string Name => _genre.Name;

    public Guid? ParentGenreId => _genre.ParentGenreId;
}