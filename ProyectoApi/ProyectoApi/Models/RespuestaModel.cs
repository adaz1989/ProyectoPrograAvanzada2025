namespace ProyectoApi.Models
{
    public class RespuestaModel
    {
        public bool Exito { get; set; }
        // No modificar Exito a Indicador porque revientan todos los métodos. -S
        public string? Mensaje { get; set; }
        public object? Datos { get; set; }


    }
}
