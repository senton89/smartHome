using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHomeSystem.Infrastructure;

namespace SmartHomeSystem.APIs
{
    public abstract class YandexIntegrationsServiceBase : IYandexIntegrationsService
    {
        protected readonly SmartHomeSystemDbContext _context;
        private readonly HttpClient _httpClient;

        public YandexIntegrationsServiceBase(
            SmartHomeSystemDbContext context,
            HttpClient httpClient
        )
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<string> DisableStation(string data)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(new { data }),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(
                "http://localhost:5001/api/yandex/disable-station",
                content
            );

            response.EnsureSuccessStatusCode(); // выбросит исключение, если код ответа не 2xx

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> IncreaseVolume(string data)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(new { data }),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(
                "http://localhost:5001/api/yandex/increase-volume",
                content
            );

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SayHello(string data)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(new { data }),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(
                "http://localhost:5001/api/yandex/say-hello",
                content
            );

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
