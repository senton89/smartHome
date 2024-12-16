using SmartHomeSystem.APIs;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class YandexIntegrationsServiceBase : IYandexIntegrationsService
{
    protected readonly SmartHomeSystemDbContext _context;

    public YandexIntegrationsServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }
}
