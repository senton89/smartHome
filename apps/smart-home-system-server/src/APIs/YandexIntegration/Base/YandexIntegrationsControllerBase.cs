using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class YandexIntegrationsControllerBase : ControllerBase
{
    protected readonly IYandexIntegrationsService _service;

    public YandexIntegrationsControllerBase(IYandexIntegrationsService service)
    {
        _service = service;
    }

    [HttpPost("disable-station")]
    public async Task<string> DisableStation()
    {
        return await _service.DisableStation("disable-station");
    }

    [HttpPost("increase-volume")]
    public async Task<string> IncreaseVolume()
    {
        return await _service.IncreaseVolume("increase-volume");
    }

    [HttpPost("say-hello")]
    public async Task<string> SayHello()
    {
        return await _service.SayHello("hello");
    }
}
