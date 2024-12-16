using Microsoft.AspNetCore.Mvc;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class RoomsController : RoomsControllerBase
{
    public RoomsController(IRoomsService service)
        : base(service) { }
}
