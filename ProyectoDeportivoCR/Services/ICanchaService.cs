using ProyectoDeportivoCR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Services
{
    public interface ICanchaService
    {
        Task<Respuesta2Model<CanchaModel>> RegistrarCancha(CanchaModel model);
        Task<Respuesta2Model<CanchaModel>> ActualizarInformacionCancha(CanchaModel model);
        Task<Respuesta2Model<CanchaModel>> DeshabilitarCancha(int canchaId);
        Task<Respuesta2Model<CanchaModel>> ObtenerCancha(int canchaId);
        Task<Respuesta2Model<List<CanchaModel>>> ObtenerTodasLasCanchas();
    }
}