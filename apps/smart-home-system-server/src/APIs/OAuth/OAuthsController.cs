using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.APIs;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class OAuthsController : OAuthsControllerBase
{
    public OAuthsController(IOAuthsService service)
        : base(service) { }
}
