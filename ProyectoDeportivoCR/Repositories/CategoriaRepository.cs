
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
            { "RegistrarCategoria",  $"{baseUrl}Categoria/RegistrarCategoria" },
            { "ActualizarCategoria", $"{baseUrl}Categoria/ActualizarInformacionCategoria" },
            { "ObtenerCategorias",   $"{baseUrl}Categoria/ObtenerInformacionCategoria" },
            { "DesabilitarCategoria", $"{baseUrl}Categoria/DeshabilitarCategoria" }
        };
    }

    public async Task<HttpResponseMessage> RegistrarCategoria(CategoriaModel model)
    {
        using var http = _httpClient.CreateClient();
        var url = _apiEndpoints["RegistrarCategoria"];
        return await http.PutAsJsonAsync(url, model);
    }

    public async Task<HttpResponseMessage> ActualizarCategoria(CategoriaModel model)
    {
        using var http = _httpClient.CreateClient();
        var url = _apiEndpoints["ActualizarCategoria"];
        return await http.PutAsJsonAsync(url, model);
    }

    public async Task<HttpResponseMessage> ObtenerCategorias(int categoriaId)
    {
        using var http = _httpClient.CreateClient();
        var url = $"{_apiEndpoints["ObtenerCategorias"]}/{categoriaId}";
        return await http.GetAsync(url);
    }

    public async Task<HttpResponseMessage> DesabilitarCategoria(int categoriaId)
    {
        using var http = _httpClient.CreateClient();
        var url = $"{_apiEndpoints["DesabilitarCategoria"]}/{categoriaId}";
        return await http.DeleteAsync(url);
    }
}
