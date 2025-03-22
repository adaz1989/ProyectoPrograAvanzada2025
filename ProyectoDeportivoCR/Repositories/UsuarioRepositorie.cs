
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
                { "IniciarSesion",    $"{baseUrl}Sesion/IniciarSesion" }
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
    }
}
