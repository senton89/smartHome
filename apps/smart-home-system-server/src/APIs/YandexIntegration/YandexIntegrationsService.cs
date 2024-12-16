using Microsoft.AspNetCore.Mvc;
using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs;

public class YandexIntegrationsService : YandexIntegrationsServiceBase
{
    public YandexIntegrationsService(SmartHomeSystemDbContext context)
        : base(context) { }
}
