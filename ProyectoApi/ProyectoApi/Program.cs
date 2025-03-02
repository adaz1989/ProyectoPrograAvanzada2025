using ProyectoApi.Configuration.App;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
ServiceConfigurator.ConfigureServices(builder);

var app = builder.Build();

// Configurar pipeline de middleware
AppConfigurator.ConfigureMiddleware(app);

app.Run();