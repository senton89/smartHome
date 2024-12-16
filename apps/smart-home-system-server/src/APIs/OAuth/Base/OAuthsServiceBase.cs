using SmartHomeSystem.APIs;
using SmartHomeSystem.Infrastructure;
using SmartHomeSystem.Infrastructure.Models;

namespace SmartHomeSystem.APIs;

public abstract class OAuthsServiceBase : IOAuthsService
{
    protected readonly SmartHomeSystemDbContext _context;

    public OAuthsServiceBase(SmartHomeSystemDbContext context)
    {
        _context = context;
    }
}
