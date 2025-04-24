using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;

namespace ProyectoApi.Services
{
    public class ReservacionService : IReservacionService
    {
        private readonly IReservacionRepository _reservacionRepository;
        private readonly IJwtService _jwtService;

        public ReservacionService(IReservacionRepository reservacionRepository, IJwtService jwtService)
        {
            _reservacionRepository = reservacionRepository;
            _jwtService = jwtService;
        }

        public async Task<RespuestaModel> RegistrarReservacion(ReservacionCanchaModel model, HttpContext httpContext)
        {
            var usuarioId = _jwtService.ObtenerUsuarioJwt(httpContext.User.Claims);
            model.UsuarioId = usuarioId;

            // hora de inicio debe ser anterior a hora de fin
            if (model.HoraInicio >= model.HoraFin)
            {
                return new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "La hora de inicio debe ser anterior a la hora de fin."
                };
            }

            // Validación 2: no solaparse con otras reservas
            var existentes = await _reservacionRepository
                .ObtenerReservacionesPorFecha(model.FechaReservavion, model.CanchaId);

            bool hayChoque = existentes.Any(r =>
                model.HoraInicio < r.HoraFin &&
                model.HoraFin > r.HoraInicio
            );

            if (hayChoque)
            {
                return new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "Ya existe una reservación en ese rango de hora para la cancha seleccionada."
                };
            }

            // Si pasa ambas validaciones, procedemos al SP de registro
            var (CodigoError, Mensaje) = await _reservacionRepository
                .RegistrarReservacion(model);

            return CodigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                _ => new RespuestaModel { Exito = false, Mensaje = Mensaje }
            };
        }


        public async Task<RespuestaModel> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId)
        {
            var resultado = await _reservacionRepository.ObtenerReservacionesPorFecha(fecha, canchaId);
            var respuesta = new RespuestaModel();

            if (resultado.Any())
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontraron reservaciones para la fecha y cancha seleccionadas";
            }

            return respuesta;
        }

        public async Task<RespuestaModel> DeshabilitarReservacion(long reservacionId)
        {
            var (codigo, mensaje) = await _reservacionRepository.DeshabilitarReservacion(reservacionId);

            return codigo switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = mensaje },
                _ => new RespuestaModel { Exito = false, Mensaje = mensaje }
            };
        }



    }
}
