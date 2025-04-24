using ProyectoDeportivoCR.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<ITorneoService, TorneoService>();
builder.Services.AddScoped<IEncriptacionService, EncriptacionService>();
builder.Services.AddScoped<IUsuarioRepositorie, UsuarioRepositorie>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// -------------------Agregados por Josue -------------------------
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IDeporteService, DeporteService>();
builder.Services.AddScoped<IDeporteRepository, DeporteRepository>();

builder.Services.AddScoped<ICanchaRepository, CanchaRepository>();
builder.Services.AddScoped<ICanchaService, CanchaService>();

builder.Services.AddScoped<DiasService>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
builder.Services.AddScoped<ICantonRepository, CantonRepository>();
builder.Services.AddScoped<IDistritoRepository, DistritoRepository>();
builder.Services.AddScoped<ICanchaRepository, CanchaRepository>();

builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<ICantonService, CantonService>();
builder.Services.AddScoped<IDistritoService, DistritoService>();
builder.Services.AddScoped<ICanchaService, CanchaService>();

builder.Services.AddScoped<IResennaService, ResennaService>();
builder.Services.AddScoped<IResennaRepository, ResennaRepository>();



//-----------------------------------------------------------------

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSession();
app.UseExceptionHandler("/Error/CapturarError");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpMethodOverride();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
