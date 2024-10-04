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

    [HttpGet("{id}")]
    public async Task<ActionResult<Platform>> GetByIdAsync([FromRoute] int id)
    {
        var platform = await _platformService.GetByIdAsync(id);

        return platform is null ? NotFound() : Ok(platform);
    }
}