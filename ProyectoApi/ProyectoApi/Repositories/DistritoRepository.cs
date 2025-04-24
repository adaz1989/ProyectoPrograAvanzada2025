
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly IDapperContext _context;

        public DistritoRepository(IDapperContext context) { 
        
            _context = context;
        }
        public async Task<IEnumerable<DistritoModel>> ObtenerTodosDistritos()
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryAsync<DistritoModel>(
                "dbo.ObtenerTodosDistritos",
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }
    }
}
