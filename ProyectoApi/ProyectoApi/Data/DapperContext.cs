using Microsoft.Data.SqlClient;
using System.Data;

namespace ProyectoApi.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BDConnection");
        }

        public IDbConnection CrearConexion() => new SqlConnection(_connectionString);
    }
}
