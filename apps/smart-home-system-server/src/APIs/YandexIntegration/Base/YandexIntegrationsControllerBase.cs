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
}
