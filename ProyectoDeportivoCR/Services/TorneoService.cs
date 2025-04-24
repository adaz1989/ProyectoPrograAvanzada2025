using ProyectoDeportivoCR.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class TorneoService : ITorneoService
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public TorneoService(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<TorneoModel> ConsultarDatosTorneos(long TorneoId)
        {
            using (var http = _httpClient.CreateClient())
            {
                var url = _configuration.GetSection("Variables:urlWebApi").Value + "Torneos/ConsultarTorneos?TorneoId=" + TorneoId;

                Console.WriteLine("URL generada: " + url);

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext?.Session.GetString("Token"));
                var response = http.GetAsync(url).Result;

                Console.WriteLine("Código de estado: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {

                    var result = response.Content.ReadFromJsonAsync<RespuestaModel>().Result;
                    
                    if (result != null && result.Indicador)
                    {
                        return JsonSerializer.Deserialize<List<TorneoModel>>((JsonElement)result.Datos!)!;
                    }

                }

                return new List<TorneoModel>();
            }
        }

    }
}
