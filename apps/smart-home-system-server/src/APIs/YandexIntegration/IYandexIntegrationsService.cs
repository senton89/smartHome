namespace SmartHomeSystem.APIs;

public interface IYandexIntegrationsService
{
    public Task<string> DisableStation(string data);
    public Task<string> IncreaseVolume(string data);
    public Task<string> SayHello(string data);
}
