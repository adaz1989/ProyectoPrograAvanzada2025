
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;
    private readonly Dictionary<string, string> _apiEndpoints;

    public CategoriaRepository(IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        var baseUrl = _configuration.GetSection("Variables:urlWebApi").Value!;

        _apiEndpoints = new Dictionary<string, string>
        {
            { "RegistrarCategoria",        $"{baseUrl}Categoria/RegistrarCategoria" },
            { "ActualizarCategoria",       $"{baseUrl}Categoria/ActualizarInformacionCategoria" },
            { "ObtenerCategorias",         $"{baseUrl}Categoria/ObtenerInformacionCategoria" },
            { "DesabilitarCategoria",      $"{baseUrl}Categoria/DeshabilitarCategoria" },
            { "ObtenerTodasLasCategorias", $"{baseUrl}Categoria/ObtenerTodasLasCategorias" }
        };
    }

    public async Task<HttpResponseMessage> RegistrarCategoria(CategoriaModel model, string? token)
    {
        using var http = _httpClient.CreateClient();
        var url = _apiEndpoints["RegistrarCategoria"];

        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await http.PutAsJsonAsync(url, model);
    }

    public async Task<HttpResponseMessage> ActualizarCategoria(CategoriaModel model, string? token)
    {
        using var http = _httpClient.CreateClient();
        var url = _apiEndpoints["ActualizarCategoria"];

        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await http.PutAsJsonAsync(url, model);
    }

    public async Task<HttpResponseMessage> ObtenerCategorias(int categoriaId, string? token)
    {
        using var http = _httpClient.CreateClient();
        var url = $"{_apiEndpoints["ObtenerCategorias"]}/{categoriaId}";

        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await http.GetAsync(url);
    }

    public async Task<HttpResponseMessage> DesabilitarCategoria(int categoriaId, string? token)
    {
        using var http = _httpClient.CreateClient();
        var url = $"{_apiEndpoints["DesabilitarCategoria"]}/{categoriaId}";

        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await http.PutAsync(url, content: null);
    }

    public async Task<HttpResponseMessage> ObtenerTodasLasCategorias(string? token)
    {
        using var http = _httpClient.CreateClient();
        var url = _apiEndpoints["ObtenerTodasLasCategorias"];  // Ruta configurada en _apiEndpoints

        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Realizamos una petición GET para obtener la lista de todas las canchas activas
        return await http.GetAsync(url);
    }
}
