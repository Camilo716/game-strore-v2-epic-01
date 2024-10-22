using GameStore.Api.Dtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class GenresController(IGenreService genreService)
    : ControllerBase
{
    private readonly IGenreService _genreService = genreService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Genre>> GetById([FromRoute] Guid id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        var response = new GenreResponseDto(genre);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
        var genres = await _genreService.GetAllAsync();
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{parentId}/genres")]
    public async Task<ActionResult<IEnumerable<Genre>>> GetByParentId([FromRoute] Guid parentId)
    {
        var childrenGenres = await _genreService.GetByParentIdAsync(parentId);
        var response = childrenGenres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _genreService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GenreCreationDto genreCreationDto)
    {
        var genre = new Genre()
        {
            Id = genreCreationDto.Genre.Id,
            Name = genreCreationDto.Genre.Name,
        };

        await _genreService.CreateAsync(genre);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = genre.Id },
            value: genre);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] GenreCreationDto genreUpdateDto)
    {
        var genre = new Genre()
        {
            Id = genreUpdateDto.Genre.Id,
            Name = genreUpdateDto.Genre.Name,
        };

        await _genreService.UpdateAsync(genre);

        return Ok();
    }
}