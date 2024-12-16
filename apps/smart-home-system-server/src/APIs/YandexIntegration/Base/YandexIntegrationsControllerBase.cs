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
    public async Task<string> DisableStation([FromBody()] string data)
    {
        return await _service.DisableStation(data);
    }

    [HttpPost("increase-volume")]
    public async Task<string> IncreaseVolume([FromBody()] string data)
    {
        return await _service.IncreaseVolume(data);
    }

    [HttpPost("say-hello")]
    public async Task<string> SayHello([FromBody()] string data)
    {
        return await _service.SayHello(data);
    }
}
