using System.Net.Http.Json;

namespace ProyectoDeportivoCR.Repositories
{
    public class CanchaRepository : ICanchaRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public CanchaRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Leer la URL base de tu archivo de configuración (appsettings.json)
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Definir los endpoints de Canchas según tu CanchasController
            _apiEndpoints = new Dictionary<string, string>
{
                { "RegistrarCancha",            $"{baseUrl}Canchas/RegistrarCancha" },
                { "ActualizarCancha",           $"{baseUrl}Canchas/ActualizarInformacionCanchas" },
                { "DeshabilitarCancha",         $"{baseUrl}Canchas/DeshabilitarCanchas" },
                { "ObtenerCancha",              $"{baseUrl}Canchas/ObtenerInformacionCanchas" },
                { "ObtenerTodasLasCanchas",     $"{baseUrl}Canchas/ObtenerTodasLasCanchas" }
            };
        }

        public async Task<HttpResponseMessage> RegistrarCancha(CanchaModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarCancha"];

            // El controlador usa [HttpPut("RegistrarCancha")]
            // Enviamos el modelo en el cuerpo de la solicitud como JSON
            return await http.PutAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> ActualizarInformacionCancha(CanchaModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ActualizarInformacionCancha"];

            // El controlador usa [HttpPut("ActualizarInformacionCanchas")]
            // Enviamos el modelo en el cuerpo de la solicitud como JSON
            return await http.PutAsJsonAsync(url, model);
        } 

        public async Task<HttpResponseMessage> DeshabilitarCancha(int canchaId)
        {
            using var http = _httpClient.CreateClient();
            // La ruta de la API es [HttpPut("DeshabilitarCanchas/{canchaId}")]
            // Se pasa el ID en la URL
            var url = $"{_apiEndpoints["DeshabilitarCancha"]}/{canchaId}";

            // No se envía cuerpo (model) porque solo deshabilitamos por ID
            // Se usa PUT con el cuerpo vacío (null) o un StringContent vacío
            return await http.PutAsync(url, content: null);
        }

        public async Task<HttpResponseMessage> ObtenerCancha(int canchaId)
        {
            using var http = _httpClient.CreateClient();
            // La ruta de la API es [HttpGet("ObtenerInformacionCanchas/{canchaId}")]
            var url = $"{_apiEndpoints["ObtenerCancha"]}/{canchaId}";

            // GET para obtener la información de la cancha
            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> ObtenerTodasLasCanchas()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodasLasCanchas"];  // Ruta configurada en _apiEndpoints

            // Realizamos una petición GET para obtener la lista de todas las canchas activas
            return await http.GetAsync(url);
        }

    }
}
