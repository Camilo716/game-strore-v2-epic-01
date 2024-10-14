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
        return Ok(genre);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
        var genres = await _genreService.GetAllAsync();
        return Ok(genres);
    }

    [HttpGet]
    [Route("{parentId}/genres")]
    public async Task<ActionResult<IEnumerable<Genre>>> GetByParentId([FromRoute] Guid parentId)
    {
        var childrenGenres = await _genreService.GetByParentIdAsync(parentId);
        return Ok(childrenGenres);
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
}