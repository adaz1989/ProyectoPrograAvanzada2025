namespace ProyectoApi.Services
{
    public class HorariosCanchaService : IHorariosCanchasService
    {
        private readonly IHorarioCanchaRepository _horarioCanchaRepository;
        private readonly IJwtService _jwtService;

        public HorariosCanchaService(IHorarioCanchaRepository horarioCanchaRepository, IJwtService jswtService)
        {
            _horarioCanchaRepository = horarioCanchaRepository;
            _jwtService = jswtService;
        }

        public async Task<RespuestaModel> ObtenerHorariosCancha(long canchaId)
        {
            var resultado = await _horarioCanchaRepository.ObtenerHorariosCancha(canchaId);

            var respuesta = new RespuestaModel();
            
            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontro horarios para esa cancha";
            }
            return (respuesta);            
        }

        public async Task<RespuestaModel> RegistrarHorarioCancha(HorarioCanchaModel model)
        {
            var (CodigoError, Mensaje) = await _horarioCanchaRepository.RegistrarHorarioCancha(model);
            return CodigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                _ => new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "Error inesperado en la base de datos"
                }
            };


        }
    }
}
