using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class PlatformsController(
    IPlatformService platformService,
    IGameService gameService)
    : ControllerBase
{
    private readonly IPlatformService _platformService = platformService;
    private readonly IGameService _gameService = gameService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Platform>> GetById([FromRoute] Guid id)
    {
        var platform = await _platformService.GetByIdAsync(id);

        return platform is null ? NotFound() : Ok(platform);
    }

    [HttpGet]
    [Route("{id}/games")]
    public async Task<ActionResult<IEnumerable<Game>>> GetGamesByPlatformId([FromRoute] Guid id)
    {
        var games = await _gameService.GetByPlatformIdAsync(id);

        return Ok(games);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Platform>>> GetAll()
    {
        var platforms = await _platformService.GetAllAsync();

        return Ok(platforms);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _platformService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PlatformPostRequest platformCreationDto)
    {
        if (!platformCreationDto.IsValid())
        {
            return BadRequest();
        }

        var platform = new Platform()
        {
            Type = platformCreationDto.Platform.Type,
        };

        await _platformService.CreateAsync(platform);

        var createdPlatform = await _platformService.GetByIdAsync(platform.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdPlatform.Id }, createdPlatform);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] PlatformPutRequest platformUpdateDto)
    {
        if (!platformUpdateDto.IsValid())
        {
            return BadRequest();
        }

        var platform = new Platform()
        {
            Id = platformUpdateDto.Platform.Id,
            Type = platformUpdateDto.Platform.Type,
        };

        await _platformService.UpdateAsync(platform);

        return Ok();
    }
}