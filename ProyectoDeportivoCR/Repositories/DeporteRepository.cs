using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Repositories
{
    public class DeporteRepository : IDeporteRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public DeporteRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Obtener la URL base desde tu configuración
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Diccionario con las rutas de la API para Deporte
            _apiEndpoints = new Dictionary<string, string>
            {
                { "RegistrarDeporte",           $"{baseUrl}Deporte/RegistrarDeporte" },
                { "ObtenerInformacionDeporte" , $"{baseUrl}Deporte/ObtenerInformacionDeporte" },
                { "ObtenerTodosLosDeportes"   , $"{baseUrl}Deporte/ObtenerTodosLosDeportes" }
            };
        }

        public async Task<HttpResponseMessage> RegistrarDeporte(DeporteModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarDeporte"];
            return await http.PutAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> ObtenerInformacionDeporte(int deporteId)
        {
            using var http = _httpClient.CreateClient();
            var url = $"{_apiEndpoints["ObtenerInformacionDeporte"]}/{deporteId}";
            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> ObtenerTodosLosDeportes()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodosLosDeportes"];  // Ruta configurada en _apiEndpoints

            // Realizamos una petición GET para obtener la lista de todas las canchas activas
            return await http.GetAsync(url);
        }
    }
}
