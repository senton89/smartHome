using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class YandexIntegrationsController : YandexIntegrationsControllerBase
{
    public YandexIntegrationsController(IYandexIntegrationsService service)
        : base(service) { }
}
