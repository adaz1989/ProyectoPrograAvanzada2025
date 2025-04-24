using System.Net.Http.Headers;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class ReservacionService : IReservacionService
    {
        private readonly IReservacionRepositorie _reservacionRepositorie;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservacionService(IReservacionRepositorie repositorie, IHttpContextAccessor httpContextAccessor)
        {
            _reservacionRepositorie = repositorie;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respuesta2Model<List<ReservacionCanchaModel>>> ObtenerReservacionesPorFecha(DateTime? fecha, long canchaId)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("Token")!;

            var fechaConsulta = new DateTime();

            if (fecha != null)            
                fechaConsulta = (DateTime)fecha;            
            else            
                fechaConsulta = DateTime.Now;            

            var response = await _reservacionRepositorie.ObtenerReservacionesPorFecha(token, fechaConsulta, canchaId);

            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<List<ReservacionCanchaModel>>();

            return new Respuesta2Model<List<ReservacionCanchaModel>>
            {
                Exito = false,
                Mensaje = "Error de comunicación con la API."
            };
        }

        public async Task<Respuesta2Model<object>> RegistrarReservacion(ReservacionCanchaModel model)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("Token")!;
            var response = await _reservacionRepositorie.RegistrarReservacion(token, model);

            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<object>();

            return new Respuesta2Model<object>
            {
                Exito = false,
                Mensaje = "Error de comunicación con la API."
            };
        }

        public async Task<Respuesta2Model<object>> DeshabilitarReservacion(long reservacionId)
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("Token")!;
            var response = await _reservacionRepositorie.DeshabilitarReservacion(token, reservacionId);

            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<object>();

            return new Respuesta2Model<object>
            {
                Exito = false,
                Mensaje = "Error de comunicación con la API."
            };
        }
    }
}
