using ProyectoDeportivoCR.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProyectoDeportivoCR.Services
{
    public class General : IGeneral
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public General(IHttpClientFactory httpClient, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<TorneosModel> ConsultarDatosTorneos(long Id)
        {
            using (var http = _httpClient.CreateClient())
            {
                var url = _configuration.GetSection("Variables:urlWebApi").Value + "Torneos/ConsultarTorneos?Id=" + Id;

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext?.Session.GetString("Token"));
                var response = http.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<RespuestaModel>().Result;

                    if (result != null && result.Indicador)
                    {
                        return JsonSerializer.Deserialize<List<TorneosModel>>((JsonElement)result.Datos!)!;
                    }
                }

                return new List<TorneosModel>();
            }
        }
    }
}
