using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProyectoApi.Data;
using Swashbuckle.AspNetCore.Filters;
/* 
Este archivo es un módulo de configuración que organiza la inicialización de:

-Controladores para manejar peticiones HTTP.
-Swagger para documentar y probar la API.
-Autenticación JWT para proteger endpoints con tokens de seguridad. 

*/
namespace ProyectoApi.Services
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            ConfigureControllers(builder);
            ConfigureSwagger(builder);
            ConfigureAuthentication(builder);
            RegisterServices(builder);
        }

        private static void ConfigureControllers(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);
            builder.Services.AddEndpointsApiExplorer();
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            string SecretKey = builder.Configuration.GetSection("Variables:llaveToken").Value!;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(SecretKey)),
                    ValidateLifetime = true,
                    LifetimeValidator = (DateTime? notBefore, DateTime? expires,
                        SecurityToken securityToken,
                        TokenValidationParameters validationParameters) =>
                    {
                        if (expires != null)
                        {
                            return expires > DateTime.UtcNow;
                        }
                        return false;
                    }
                };
            });
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {            
            builder.Services.AddScoped<IDapperContext, DapperContext>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}