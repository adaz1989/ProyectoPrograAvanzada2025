using ProyectoDeportivoCR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// 1. A�ade MVC y HttpClientFactory
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// 2. Configura sesi�n en memoria
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Tiempo de inactividad antes de expirar la sesi�n
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. Para poder inyectar IHttpContextAccessor y leer/escribir sesi�n
builder.Services.AddHttpContextAccessor();

// 4. Inyecci�n de dependencias de tus servicios/repositorios
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
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<ICantonService, CantonService>();
builder.Services.AddScoped<IDistritoService, DistritoService>();
builder.Services.AddScoped<ICanchaService, CanchaService>();

builder.Services.AddScoped<IResennaService, ResennaService>();
builder.Services.AddScoped<IResennaRepository, ResennaRepository>();

builder.Services.AddScoped<IReservacionService, ReservacionService>();
builder.Services.AddScoped<IReservacionRepositorie, ReservacionRepositorie>();


//-----------------------------------------------------------------

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ** Importante: ** UseSession **antes** de UseAuthorization
app.UseSession();
app.UseAuthorization();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
