using AutoMapper;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(
    IMapper mapper,
    IGameService gameService,
    IGenreService genreService,
    IPlatformService platformService)
    : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameService _gameService = gameService;
    private readonly IGenreService _genreService = genreService;
    private readonly IPlatformService _platformService = platformService;

    [HttpGet]
    public async Task<ActionResult<Game>> Get()
    {
        var games = await _gameService.GetAllAsync();
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Game>> GetAsync([FromRoute] Guid id)
    {
        var game = await _gameService.GetByIdAsync(id);
        return Ok(new GameResponseDto(game));
    }

    [HttpGet]
    [Route("{key}/genres")]
    public async Task<ActionResult<Genre>> GetGenresByGameKey([FromRoute] string key)
    {
        var genres = await _genreService.GetByGameKeyAsync(key);
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{key}/platforms")]
    public async Task<ActionResult<Platform>> GetPlatformsByGameKey([FromRoute] string key)
    {
        var platforms = await _platformService.GetByGameKeyAsync(key);
        var response = platforms.Select(p => new PlatformResponseDto(p));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> Post([FromBody] GamePostRequest gamePostDto)
    {
        var game = _mapper.Map<Game>(gamePostDto);

        await _gameService.CreateAsync(game);

        // TODO: Use GetById when implemented
        return CreatedAtAction(nameof(Get), value: game);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] GamePutRequest gamePutRequest)
    {
        var game = _mapper.Map<Game>(gamePutRequest);

        await _gameService.UpdateAsync(game);

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _gameService.DeleteAsync(id);
        return NoContent();
    }
}