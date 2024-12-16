using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class DevicesService : DevicesServiceBase
{
    public DevicesService(SmartHomeSystemDbContext context)
        : base(context) { }
}
