using Microsoft.AspNetCore.Mvc;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class DevicesController : DevicesControllerBase
{
    public DevicesController(IDevicesService service)
        : base(service) { }
}
