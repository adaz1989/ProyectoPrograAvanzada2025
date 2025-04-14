using System.Net.Http.Json;

namespace ProyectoDeportivoCR.Repositories
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public ProvinciaRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Leer la URL base del archivo de configuración
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Definir los endpoints de Provincias según el controller correspondiente
            _apiEndpoints = new Dictionary<string, string>
            {
                { "ObtenerTodasProvincias", $"{baseUrl}Provincia/ObtenerTodasProvincias" }
            };
        }

        public async Task<HttpResponseMessage> ObtenerTodasProvincias()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodasProvincias"];
            // Se realiza una petición GET para obtener la lista de provincias
            return await http.GetAsync(url);
        }
    }
}
