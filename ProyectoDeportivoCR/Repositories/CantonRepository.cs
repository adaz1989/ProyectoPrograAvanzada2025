using System.Net.Http.Json;

namespace ProyectoDeportivoCR.Repositories
{
    public class CantonRepository : ICantonRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public CantonRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Leer la URL base del archivo de configuración
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Definir los endpoints de Cantones según el controller correspondiente
            _apiEndpoints = new Dictionary<string, string>
            {
                { "ObtenerTodosCantones", $"{baseUrl}Canton/ObtenerTodosCantones" }
            };
        }

        public async Task<HttpResponseMessage> ObtenerTodosCantones()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodosCantones"];
            // Se realiza una petición GET para obtener la lista de cantones
            return await http.GetAsync(url);
        }
    }
}
