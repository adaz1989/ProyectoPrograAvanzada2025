using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ProyectoDeportivoCR.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public FacturaRepository(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // Obtenemos la URL base configurada, por ejemplo "https://tuservidor.com/"
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

            // Se definen los endpoints de la API, de acuerdo a las rutas del FacturasController.
            _apiEndpoints = new Dictionary<string, string>
{
                { "RegistrarFactura", $"{baseUrl}Facturas/RegistrarFactura" },
                { "ObtenerFacturaPorId", $"{baseUrl}Facturas/ObtenerFacturaPorId" },
                { "ObtenerTodasLasFacturas", $"{baseUrl}Facturas/ObtenerTodasLasFacturas" }
            };
        }

        // Método para registrar una factura utilizando PUT
        public async Task<HttpResponseMessage> RegistrarFactura(FacturaModel model, string? token)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarFactura"];

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                model.FacturaId,
                model.Monto,
                model.FechaHoraFactura,
                model.FotoComprobante,
                model.Comprobante,
                model.ReservacionId,
                model.UsuarioId,
                model.MetodoPagoId,
            };

            return await http.PutAsJsonAsync(url, payload);
        }

        // Método para obtener una factura por Id utilizando GET
        public async Task<HttpResponseMessage> ObtenerFacturaPorId(int facturaId, string? token)
        {
            using var http = _httpClient.CreateClient();
            var url = $"{_apiEndpoints["ObtenerFacturaPorId"]}/{facturaId}";

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await http.GetAsync(url);
        }

        // Método para obtener todas las facturas utilizando GET
        // Notar que la ruta ya no requiere un parámetro en la URL, de acuerdo a tu controlador.
        public async Task<HttpResponseMessage> ObtenerTodasLasFacturas(string? token)
        {
            try
            {
                using var http = _httpClient.CreateClient();
                var url = _apiEndpoints["ObtenerTodasLasFacturas"];

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return await http.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al comunicarse con el API: " + ex.Message);
            }
        }


    }
}
