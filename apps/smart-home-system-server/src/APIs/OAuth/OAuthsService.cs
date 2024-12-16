using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class OAuthsService : OAuthsServiceBase
{
    public OAuthsService(SmartHomeSystemDbContext context)
        : base(context) { }
}
