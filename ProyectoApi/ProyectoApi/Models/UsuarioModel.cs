using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class UsuarioModel
    {
        public long UsuarioId { get; set; }
        [StringLength(50)]
        public string? NombreUsuario { get; set; }
        [StringLength(100)]
        public string? ApellidosUsuario { get; set; }
        [StringLength(50)]
        public string? CorreoUsuario { get; set; }
        [StringLength(50)]
        public string? TelefonoUsuario { get; set; }
        [StringLength(50)]
        public string? Contrasenna { get; set; }
        public long TipoUsuarioId { get; set; }
        public string? Token { get; set; }

        //Desnormalizacion
        public string? DescripcionTipoUsuario { get; set; }

        // * Probar usar la clase completa de tipo usuario

    }
}
