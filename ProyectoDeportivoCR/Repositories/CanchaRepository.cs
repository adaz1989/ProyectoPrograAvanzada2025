using System.Net.Http.Headers;
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

            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            _apiEndpoints = new Dictionary<string, string>
{
                { "RegistrarCancha",            $"{baseUrl}Canchas/RegistrarCancha" },
                { "ActualizarCancha",           $"{baseUrl}Canchas/ActualizarInformacionCanchas" },
                { "DeshabilitarCancha",         $"{baseUrl}Canchas/DeshabilitarCanchas" },
                { "ObtenerCancha",              $"{baseUrl}Canchas/ObtenerInformacionCanchas" },
                { "ObtenerTodasLasCanchas",     $"{baseUrl}Canchas/ObtenerTodasLasCanchas" },
                { "ObtenerHorariosCancha",      $"{baseUrl}HorariosCancha/ObtenerHorariosCancha" },
                { "RegistrarHorarioCancha",     $"{baseUrl}HorariosCancha/RegistrarHorarioCancha" }
            };
        }

        public async Task<HttpResponseMessage> RegistrarCancha(CanchaModel model, string? token)
        {
            using var http = _httpClient.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var payload = new
            {
                model.NombreCancha,
                model.CorreoCancha,
                model.TelefonoCancha,
                model.PrecioHora,
                model.DetalleDireccion,
                model.DescripcionCancha,
                model.UsuarioId,
                model.DeporteId,
                model.ProvinciaId,
                model.CantonId,
                model.DistritoId,
                model.FotoCancha
            };

            var url = _apiEndpoints["RegistrarCancha"];
            return await http.PostAsJsonAsync(url, payload);
        }

        public async Task<HttpResponseMessage> ActualizarInformacionCancha(CanchaModel model, string? token)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ActualizarCancha"];

            if (!string.IsNullOrEmpty(token))
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var payload = new
            {
                model.CanchaId,
                model.NombreCancha,
                model.CorreoCancha,
                model.TelefonoCancha,
                model.PrecioHora,
                model.DetalleDireccion,
                model.DescripcionCancha,
                model.DeporteId,
                model.FotoCancha,
                model.Estado
            };

            // El controlador usa [HttpPut("ActualizarInformacionCanchas")]
            // Enviamos el modelo en el cuerpo de la solicitud como JSON
            return await http.PutAsJsonAsync(url, payload);
        }

        public async Task<HttpResponseMessage> DeshabilitarCancha(long canchaId, string? token)
        {
            using var http = _httpClient.CreateClient();
            // La ruta de la API es [HttpPut("DeshabilitarCanchas/{canchaId}")]
            // Se pasa el ID en la URL
            var url = $"{_apiEndpoints["DeshabilitarCancha"]}/{canchaId}";

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // No se envía cuerpo (model) porque solo deshabilitamos por ID
            // Se usa PUT con el cuerpo vacío (null) o un StringContent vacío
            return await http.PutAsync(url, content: null);
        }

        public async Task<HttpResponseMessage> ObtenerCancha(long canchaId, string? token)
        {
            using var http = _httpClient.CreateClient();
            // La ruta de la API es [HttpGet("ObtenerInformacionCanchas/{canchaId}")]
            var url = $"{_apiEndpoints["ObtenerCancha"]}/{canchaId}";

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // GET para obtener la información de la cancha
            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> ObtenerHorariosCancha(long canchaId)
        {
            using var http = _httpClient.CreateClient();
            // La ruta de la API es [HttpGet("ObtenerHorariosCanchas/{canchaId}")]
            var url = $"{_apiEndpoints["ObtenerHorariosCancha"]}/{canchaId}";
            // GET para obtener la información de la cancha
            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> RegistrarHorarioCancha(HorarioCanchaModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarHorarioCancha"];
            return await http.PostAsJsonAsync(url, model);
        }
        public async Task<HttpResponseMessage> ObtenerTodasLasCanchas(string? token)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodasLasCanchas"];  // Ruta configurada en _apiEndpoints

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Realizamos una petición GET para obtener la lista de todas las canchas activas
            return await http.GetAsync(url);
        }


    }
}
