using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class UserSettingsService : UserSettingsServiceBase
{
    public UserSettingsService(SmartHomeSystemDbContext context)
        : base(context) { }
}
