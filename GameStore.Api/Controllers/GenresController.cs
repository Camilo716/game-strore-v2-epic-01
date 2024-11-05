using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class GenresController(
    IGenreService genreService,
    IGameService gameService)
    : ControllerBase
{
    private IGenreService GenreService => genreService;

    private IGameService GameService => gameService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<GenreResponseDto>> GetById([FromRoute] Guid id)
    {
        var genre = await GenreService.GetByIdAsync(id);
        var response = new GenreResponseDto(genre);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}/games")]
    public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetGamesByGenreId([FromRoute] Guid id)
    {
        var games = await GameService.GetByGenreIdAsync(id);
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> Get()
    {
        var genres = await GenreService.GetAllAsync();
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{parentId}/genres")]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetByParentId([FromRoute] Guid parentId)
    {
        var childrenGenres = await GenreService.GetByParentIdAsync(parentId);
        var response = childrenGenres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await GenreService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GenrePostRequest genrePostDto)
    {
        if (!genrePostDto.IsValid())
        {
            return BadRequest();
        }

        var genre = new Genre()
        {
            Name = genrePostDto.Genre.Name,
        };

        await GenreService.CreateAsync(genre);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = genre.Id },
            value: genre);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] GenrePutRequest genrePutDto)
    {
        if (!genrePutDto.IsValid())
        {
            return BadRequest();
        }

        var genre = new Genre()
        {
            Id = genrePutDto.Genre.Id,
            Name = genrePutDto.Genre.Name,
        };

        await GenreService.UpdateAsync(genre);

        return Ok();
    }
}