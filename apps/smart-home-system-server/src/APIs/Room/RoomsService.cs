using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(SmartHomeSystemDbContext context)
        : base(context) { }
}
