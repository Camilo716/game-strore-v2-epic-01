using GameStore.Api.Dtos;
using GameStore.Api.Dtos.GenresDtos;
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
    public async Task<ActionResult> Post([FromBody] GenrePostRequest genrePostDto)
    {
        var genre = new Genre()
        {
            Name = genrePostDto.Genre.Name,
        };

        await _genreService.CreateAsync(genre);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = genre.Id },
            value: genre);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] GenrePutRequest genrePutDto)
    {
        var genre = new Genre()
        {
            Id = genrePutDto.Genre.Id,
            Name = genrePutDto.Genre.Name,
        };

        await _genreService.UpdateAsync(genre);

        return Ok();
    }
}