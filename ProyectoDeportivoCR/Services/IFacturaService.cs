using System.Collections.Generic;

namespace ProyectoDeportivoCR.Services
{
    public interface IFacturaService
    {
        public Task<Respuesta2Model<FacturaModel>> RegistrarFactura(FacturaModel model);

        public Task<Respuesta2Model<FacturaModel>> ObtenerFacturaPorId(int facturaId);

        public Task<Respuesta2Model<List<FacturaModel>>> ObtenerTodasLasFacturas();


    }
}
