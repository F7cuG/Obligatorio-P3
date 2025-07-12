using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Services
{
    public class ClimaService
    {
        private readonly string _baseUrl = ConfigurationManager.AppSettings["OpenWeatherMap:BaseUrl"];
        private readonly string _apiKey = ConfigurationManager.AppSettings["OpenWeatherMap:ApiKey"];

        public async Task<ClimaInfo> GetWeatherByCityAsync(string ciudad)
        {
            if (string.IsNullOrWhiteSpace(ciudad))
                throw new ArgumentException("Ciudad vacía", nameof(ciudad));
           
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}/weather?q={Uri.EscapeDataString(ciudad)}&units=metric&appid={_apiKey}&lang=es";
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ClimaInfo>(json);
            }

        }
    }
}
