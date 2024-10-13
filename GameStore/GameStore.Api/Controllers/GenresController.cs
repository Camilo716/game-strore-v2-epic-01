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
    public async Task<ActionResult<Genre>> Get([FromRoute] Guid id)
    {
        var genre = await _genreService.GetByIdAsync(id);
        return Ok(genre);
    }
}