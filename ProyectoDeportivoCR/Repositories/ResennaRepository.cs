using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProyectoDeportivoCR.Models; // Asegúrate del namespace correcto para ResennaCanchaModel

namespace ProyectoDeportivoCR.Repositories
{
    public class ResennaRepository : IResennaRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public ResennaRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration.GetValue<string>("Variables:urlWebApi")!;

            _apiEndpoints = new Dictionary<string, string>
            {
                { "RegistrarResenna",           $"{baseUrl}Resennas/RegistrarResenna" },
                { "ObtenerResennaPorCancha",    $"{baseUrl}Resennas/ObtenerResennaPorCancha" },
                { "ObtenerTodasLasResennas",    $"{baseUrl}Resennas/ObtenerTodasLasResennas" },
            };
        }

        public async Task<HttpResponseMessage> RegistrarResenna(ResennaCanchaModel model, string? token)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Enviar el modelo de reseña como JSON
            return await http.PostAsJsonAsync(_apiEndpoints["RegistrarResenna"], model);
        }

        public async Task<HttpResponseMessage> ObtenerResennaPorCancha(long canchaId, string? token)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Solicitud GET con el parámetro en la ruta
            var url = $"{_apiEndpoints["ObtenerResennaPorCancha"]}/{canchaId}";
            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> ObtenerTodasLasResennas(string? token)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Solicitud GET al endpoint de todas las reseñas
            return await http.GetAsync(_apiEndpoints["ObtenerTodasLasResennas"]);
        }
    }
}
