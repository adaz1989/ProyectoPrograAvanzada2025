
using System.Net.Http.Headers;

namespace ProyectoDeportivoCR.Repositories
{
    public class UsuarioRepositorie : IUsuarioRepositorie
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _apiEndpoints;

        public UsuarioRepositorie(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;
            _apiEndpoints = new Dictionary<string, string>
            {
                { "RegistrarUsuario", $"{baseUrl}Sesion/RegistrarUsuario" },
                { "IniciarSesion",    $"{baseUrl}Sesion/IniciarSesion" },
                { "ObtenerInformacionUsuario", $"{baseUrl}Usuario/ObtenerInformacionUsuario" },
                { "ActualizarInformacionUsuario",$"{baseUrl}Usuario/ActualizarInformacionUsuario" }
            };
        }

        public async Task<HttpResponseMessage> IniciarSesion(UsuarioModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["IniciarSesion"];
            return await http.PostAsJsonAsync(url, model);

        }

        public async Task<HttpResponseMessage> RegistrarUsuario(UsuarioModel model)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["RegistrarUsuario"];            
            return await http.PostAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> ObtenerInformacionUsuario(string token)
        {
            using var http = _httpClient.CreateClient();
            var url = _apiEndpoints["ObtenerInformacionUsuario"];

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await http.GetAsync(url);
        }

        public async Task<HttpResponseMessage> ActualizarInformacionUsuario(string token, UsuarioModel model)
        {
            using var http = _httpClient.CreateClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = _apiEndpoints["ActualizarInformacionUsuario"];
            return await http.PutAsJsonAsync(url, model);
        }
    }
}
