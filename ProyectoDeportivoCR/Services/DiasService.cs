using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class DiasService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public DiasService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;
            _apiEndpoints = new Dictionary<string, string>
                    {
                        { "ObtenerDias", $"{baseUrl}Dia/ObtenerDias" }
                    };
        }

        public async Task<Respuesta2Model<List<DiaModel>>> ObtenerDias()
        {
            using var http = _httpClient.CreateClient();  
            var url = _apiEndpoints["ObtenerDias"];

            var respuestaJson =  await http.GetAsync(url);

            var respuesta = await respuestaJson.LeerRespuesta2Model<List<DiaModel>>();

            return respuesta;
        }
    }
}
