namespace ProyectoApi.Services
{
    public interface IJwtService
    {
        string GenerarToken(long usuarioId, string DescripcionTipoUsuario);
    }
}
