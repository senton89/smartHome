using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;

namespace SmartHomeSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OAuthsControllerBase : ControllerBase
{
    protected readonly IOAuthsService _service;

    public OAuthsControllerBase(IOAuthsService service)
    {
        _service = service;
    }
}
