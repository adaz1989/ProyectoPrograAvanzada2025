using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoDeportivoCR.Models;
using ProyectoDeportivoCR.Models.ViewModels;
using ProyectoDeportivoCR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Controllers
{
    public class ReservacionController : Controller
    {
        private readonly ICanchaService _canchaService;
        private readonly IReservacionService _reservacionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservacionController(
            ICanchaService canchaService,
            IReservacionService reservacionService,
            IHttpContextAccessor httpContextAccessor)
        {
            _canchaService = canchaService;
            _reservacionService = reservacionService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Reservacion(DateTime? fecha, long canchaId)
        {
            // 1. Fecha de consulta
            var fechaConsulta = fecha?.Date ?? DateTime.Today;

            // 2. Obtener datos de la cancha
            var respCancha = await _canchaService.ObtenerCancha(canchaId);
            if (!respCancha.Exito)
            {
                TempData["ErrorMessage"] = respCancha.Mensaje;
                return RedirectToAction("Index", "Cancha");
            }
            var cancha = respCancha.Datos!;

            // 3. Inicializar ViewModel
            var vm = new ReservacionViewModel
            {
                Cancha = cancha,
                Fecha = fechaConsulta,
                Slots = new List<TimeSlotViewModel>(),
                HoraInicioOptions = new List<SelectListItem>(),
                HoraFinOptions = new List<SelectListItem>()
            };

            IEnumerable<ReservacionCanchaModel> reservas = Enumerable.Empty<ReservacionCanchaModel>();
            IEnumerable<HorarioCanchaModel> horarios = Enumerable.Empty<HorarioCanchaModel>();

            // 4. Obtener reservas
            var respReservas = await _reservacionService.ObtenerReservacionesPorFecha(fechaConsulta, canchaId);
            if (respReservas.Exito && respReservas.Datos is { Count: > 0 })
            {
                reservas = respReservas.Datos;
            }
            else
            {
                ViewBag.ErrorMessage = respReservas.Mensaje ?? "No hay reservas para la fecha seleccionada.";
            }

            // 5. Obtener horarios de la cancha
            var respHorarios = await _canchaService.ObtenerHorariosCancha(canchaId);
            if (respHorarios.Exito && respHorarios.Datos is { Count: > 0 })
            {
                horarios = respHorarios.Datos;
            }
            else
            {
                ViewBag.ErrorMessage = respHorarios.Exito
                    ? "No se encontraron horarios para esta cancha."
                    : respHorarios.Mensaje;
            }

            // 6. Mapear DayOfWeek: domingo=0→7, lunes=1, … sábado=6
            int diaSemana = fechaConsulta.DayOfWeek == DayOfWeek.Sunday
                               ? 7
                               : (int)fechaConsulta.DayOfWeek;

            // 7. Encontrar el horario para ese día
            var horarioDia = horarios.FirstOrDefault(h => h.DiaId == diaSemana);
            if (horarioDia is null)
            {
                ViewBag.ErrorMessage = "No hay horario definido para este día.";
            }
            else
            {
                // 8. Poblar los dropdowns de horas (Value="HH:mm:ss", Text="HH:mm")
                var listaTiempos = new List<SelectListItem>();
                var t = new TimeOnly(horarioDia.HoraApertura.Hour, 0);
                var cierre = horarioDia.HoraCierre;

                while (t < cierre)
                {
                    listaTiempos.Add(new SelectListItem
                    {
                        Value = t.ToString("HH:mm:ss"),
                        Text = t.ToString("HH:mm")
                    });
                    t = t.AddHours(1);
                }

                vm.HoraInicioOptions = listaTiempos;
                vm.HoraFinOptions = listaTiempos;

                // 9. Obtener el usuario logueado
                var userIdClaim = _httpContextAccessor.HttpContext?.User
                                        .FindFirst("UsuarioId")?.Value;
                long usuarioId = userIdClaim is not null
                                 && long.TryParse(userIdClaim, out var tmp)
                                 ? tmp
                                 : 0;

                // 10. Generar slots de 1 hora marcando estado según reservas
                var horaActual = new TimeOnly(horarioDia.HoraApertura.Hour, 0);
                while (horaActual < cierre)
                {
                    var inicioSlot = horaActual;
                    var finSlot = horaActual.AddHours(1);

                    var reserva = reservas
                        .FirstOrDefault(r => r.HoraInicio == inicioSlot.ToTimeSpan());

                    string estado = reserva == null
                        ? "Disponible"
                        : reserva.UsuarioId == usuarioId
                            ? "ReservadaPorUsuario"
                            : "Ocupada";

                    vm.Slots.Add(new TimeSlotViewModel
                    {
                        HoraInicio = inicioSlot,
                        HoraFin = finSlot,
                        Estado = estado
                    });

                    horaActual = horaActual.AddHours(1);
                }
            }

            // 11. Devolver la vista con un solo return
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarReservacion(ReservacionCanchaModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Modelo incorrecto";
                return RedirectToAction("Reservacion", new
                {
                    canchaId = model.CanchaId,
                    fecha = model.FechaReservavion.ToString("yyyy-MM-dd")
                });
            }

            var resp = await _reservacionService.RegistrarReservacion(model);
            TempData["Mensaje"] = resp.Mensaje;

            return RedirectToAction("Reservacion", new
            {
                canchaId = model.CanchaId,
                fecha = model.FechaReservavion.ToString("yyyy-MM-dd")
            });
        }




    }
}
