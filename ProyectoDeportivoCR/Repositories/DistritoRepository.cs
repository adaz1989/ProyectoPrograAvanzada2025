using System.Net.Http.Json;

namespace ProyectoDeportivoCR.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public DistritoRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Leer la URL base de la configuración (appsettings.json)
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Definir los endpoints de Distritos según el controller correspondiente
            _apiEndpoints = new Dictionary<string, string>
            {
                { "ObtenerTodosDistritos", $"{baseUrl}Distrito/ObtenerTodosDistritos" }
            };
        }

        public async Task<HttpResponseMessage> ObtenerTodosDistritos()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodosDistritos"];
            // Se asume que se realiza una petición GET para obtener la lista de distritos
            return await http.GetAsync(url);
        }
    }
}
