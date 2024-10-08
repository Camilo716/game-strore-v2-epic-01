using GameStore.Api.Dtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class PlatformsController(IPlatformService platformService)
    : ControllerBase
{
    private readonly IPlatformService _platformService = platformService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Platform>> GetById([FromRoute] Guid id)
    {
        var platform = await _platformService.GetByIdAsync(id);

        return platform is null ? NotFound() : Ok(platform);
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
    public async Task<ActionResult> Post([FromBody] PlatformCreationDto platformCreationDto)
    {
        var platform = new Platform()
        {
            Id = platformCreationDto.Platform.Id,
            Type = platformCreationDto.Platform.Type,
        };

        await _platformService.CreateAsync(platform);

        var createdPlatform = await _platformService.GetByIdAsync(platform.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdPlatform.Id }, createdPlatform);
    }
}