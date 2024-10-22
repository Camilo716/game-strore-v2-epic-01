using GameStore.Api.Dtos;
using GameStore.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGenreService genreService) : ControllerBase
{
    private readonly IGenreService _genreService = genreService;

    [HttpGet]
    [Route("{key}/genres")]
    public async Task<IActionResult> GetGenresByKey([FromRoute] string key)
    {
        var genres = await _genreService.GetByGameKeyAsync(key);
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }
}