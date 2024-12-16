using Microsoft.AspNetCore.Mvc;

namespace SmartHomeSystem.APIs;

[ApiController()]
public class UserSettingsController : UserSettingsControllerBase
{
    public UserSettingsController(IUserSettingsService service)
        : base(service) { }
}
