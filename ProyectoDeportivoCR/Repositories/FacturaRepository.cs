using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        public async Task<HttpResponseMessage> RegistrarFactura(FacturaModel model)
        {
           
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarFactura"];
            return await http.PutAsJsonAsync(url, model);
        }

        // Método para obtener una factura por Id utilizando GET
        public async Task<HttpResponseMessage> ObtenerFacturaPorId(int facturaId)
        {
            using var http = _httpClient.CreateClient();
            var url = $"{_apiEndpoints["ObtenerFacturaPorId"]}/{facturaId}";
            return await http.GetAsync(url);
        }

        // Método para obtener todas las facturas utilizando GET
        // Notar que la ruta ya no requiere un parámetro en la URL, de acuerdo a tu controlador.
        public async Task<HttpResponseMessage> ObtenerTodasLasFacturas()
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerTodasLasFacturas"];
            return await http.GetAsync(url);
        }

    }
}
