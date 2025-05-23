using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

using ProyectoApi.Configuration.App;
using ProyectoApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


builder.Services.AddScoped<IDeporteRepository, DeporteRepository>();
builder.Services.AddScoped<IDeporteService, DeporteService>();

builder.Services.AddScoped<ICanchasService, CanchasService>();
builder.Services.AddScoped<ICanchasRepository, CanchasRepository>();


builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();

builder.Services.AddScoped<IFacturaService, FacturasService>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();

builder.Services.AddScoped<ICantonRepository, CantonRepository>(); 
builder.Services.AddScoped<ICantonService, CantonService>();

builder.Services.AddScoped<IProvinciaRepository,ProvinciaRepository>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();

builder.Services.AddScoped<IDistritoService, DistritoService>();
builder.Services.AddScoped<IDistritoRepository, DistritoRepository>();

builder.Services.AddScoped<IResennaCanchaService, ResennaCanchaService>();
builder.Services.AddScoped<IResennaCanchaRepository, ResennaRepository>();




builder.Services.AddScoped<IHorarioCanchaRepository, HorarioCanchaRepository>();
builder.Services.AddScoped<IHorariosCanchasService, HorariosCanchaService>();

builder.Services.AddScoped<IReservacionRepository, ReservacionRepository>();
builder.Services.AddScoped<IReservacionService, ReservacionService>();



builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

string SecretKey = builder.Configuration.GetSection("Variables:llaveToken").Value!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
        ValidateLifetime = true,
        LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        },
        NameClaimType = "UsuarioId"
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocalhost", policy =>
    {
        policy.WithOrigins("https://localhost:7041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/api/Error/CapturarError");

app.UseHttpsRedirection();
app.UseCors("AllowFrontendLocalhost");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
