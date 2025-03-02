namespace ProyectoApi.Configuration.App
{
    /* 
     * 
    Este archivo define la configuración del middleware y los endpoints.

    */
    public static class AppConfigurator
    {
        public static void ConfigureMiddleware(WebApplication app)
        {
            ConfigureDevelopmentMiddleware(app);
            ConfigureProductionMiddleware(app);
            ConfigureEndpoints(app);
        }

        private static void ConfigureDevelopmentMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        private static void ConfigureProductionMiddleware(WebApplication app)
        {
            // Configura un middleware para manejar errores y redirigir a un endpoint de captura de errores.
            app.UseExceptionHandler("/api/Error/CapturarError");

            // Fuerza el uso de HTTPS, asegurando que todas las solicitudes se redirijan a una conexión segura.
            app.UseHttpsRedirection();

            // Habilita la autorización para restringir el acceso a los endpoints protegidos.
            app.UseAuthorization();

        }

        private static void ConfigureEndpoints(WebApplication app)
        {
            //Registra y habilita los endpoints de los controladores, permitiendo que la API procese solicitudes HTTP.
            app.MapControllers();
        }
    }
}
