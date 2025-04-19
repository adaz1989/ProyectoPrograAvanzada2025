using System.Security.Claims;

namespace ProyectoApi.Services
{
    public interface IJwtService
    {
        string GenerarToken(long usuarioId, string DescripcionTipoUsuario);
        public long ObtenerUsuarioJwt(IEnumerable<Claim> valores);
    }
}
