using Microsoft.AspNetCore.Mvc;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class SchedulesController : SchedulesControllerBase
{
    public SchedulesController(ISchedulesService service)
        : base(service) { }
}
