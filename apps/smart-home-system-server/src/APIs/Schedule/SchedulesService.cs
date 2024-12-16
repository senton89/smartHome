using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class SchedulesService : SchedulesServiceBase
{
    public SchedulesService(SmartHomeSystemDbContext context)
        : base(context) { }
}
