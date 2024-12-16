using SmartHomeSystem.APIs;

namespace SmartHomeSystem;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDevicesService, DevicesService>();
        services.AddScoped<IOAuthsService, OAuthsService>();
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<ISchedulesService, SchedulesService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        services.AddScoped<IYandexIntegrationsService, YandexIntegrationsService>();
    }
}
