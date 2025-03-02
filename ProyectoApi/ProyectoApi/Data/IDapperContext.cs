using System.Data;

namespace ProyectoApi.Data
{
    public interface IDapperContext
    {
        public IDbConnection CrearConexion();
    }

}
