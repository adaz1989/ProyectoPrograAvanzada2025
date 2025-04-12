using Dapper;
using ProyectoApi.Data;
using ProyectoApi.Models;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IDapperContext _context;

        public FacturaRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarFactura(FacturaModel model)
        {
            using var conexion = _context.CrearConexion();

          
            if (model.FotoComprobante == null && !string.IsNullOrEmpty(model.FotoBase64))
            {
                try
                {
                    model.FotoComprobante = Convert.FromBase64String(model.FotoBase64);
                }
                catch (FormatException)
                {
                    return (-1, "La imagen no tiene un formato base64 válido.");
                }
            }

            var parametros = new DynamicParameters(new
            {
                model.Monto,
                model.FechaHoraFactura,
                model.Comprobante,
                model.ReservacionId,
                model.UsuarioId,
                model.MetodoPagoId,
                model.FotoComprobante
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.RegistrarFactura", parametros, commandType: CommandType.StoredProcedure);

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            return (codigoError, mensaje);
        }

        public async Task<FacturaModel> ObtenerFacturaPorId(long facturaId)
        {
            using var conexion = _context.CrearConexion();

            var parameters = new DynamicParameters();
            parameters.Add("@FacturaId", facturaId, DbType.Int64);
            // Se agregan parámetros de salida en caso de que el SP los requiera para información adicional
            parameters.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            var factura = await conexion.QueryFirstOrDefaultAsync<FacturaModel>(
                "dbo.ObtenerFacturaPorId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return factura!;
        }

        public async Task<IEnumerable<FacturaModel>> ObtenerTodasLasFacturas()
        {
            using var conexion = _context.CrearConexion();

            var facturas = await conexion.QueryAsync<FacturaModel>(
                "dbo.ObtenerTodasLasFacturas",
                commandType: CommandType.StoredProcedure
            );

            return facturas;
        }
    }
}
