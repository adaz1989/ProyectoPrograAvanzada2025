namespace ProyectoDeportivoCR.Models
{
    public class RespuestaModel<T>
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }

        // T representa un tipo genérico que permite deserializar cualquier modelo específico en la misma estructura de respuesta
        public T? Datos { get; set; }
    }
}
