using static ProyectoDeportivoCR.Repositories.ReservacionRepositorie;
using System.Net.Http.Headers;

namespace ProyectoDeportivoCR.Repositories
{
    public class ReservacionRepositorie : IReservacionRepositorie
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly Dictionary<string, string> _apiEndpoints;

        public ReservacionRepositorie(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var baseUrl = configuration.GetValue<string>("Variables:urlWebApi")!;
            _apiEndpoints = new Dictionary<string, string>
            {
                { "ObtenerReservacionesPorFecha", $"{baseUrl}Reservacion/ObtenerReservacionesPorFecha" },
                { "RegistrarReservacion",         $"{baseUrl}Reservacion/RegistrarReservacion" },
                { "DeshabilitarReservacion",      $"{baseUrl}Reservacion/DeshabilitarReservacion" }
            };
        }

        public async Task<HttpResponseMessage> ObtenerReservacionesPorFecha(string token, DateTime fecha, long canchaId)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var ruta = $"{_apiEndpoints["ObtenerReservacionesPorFecha"]}/{fecha:yyyy-MM-dd}/{canchaId}";
            return await http.GetAsync(ruta);
        }

        public async Task<HttpResponseMessage> RegistrarReservacion(string token, ReservacionCanchaModel model)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = _apiEndpoints["RegistrarReservacion"];
            return await http.PostAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> DeshabilitarReservacion(string token, long reservacionId)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var ruta = $"{_apiEndpoints["DeshabilitarReservacion"]}/{reservacionId}";
            return await http.PutAsync(ruta, null);
        }
    }
}

