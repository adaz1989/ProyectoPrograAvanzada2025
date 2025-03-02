namespace ProyectoApi.Models
{
    public class ResultadoTorneoModel
    {
        public long TorneoId { get; set; }
        public int NumeroPartida { get; set; }
        public long ReservacionId { get; set; }        
        public int PuntosEquipo1 { get; set; }        
        public int PuntosEquipo2 { get; set; }
        // Llaves foraneas
        public long EquipoId1 { get; set; }
        public long EquipoId2 { get; set; }

        // Desnormalizacion
        public string? NombreEquipo1 { get; set; }
        public string? NombreEquipo2 { get; set; }
    }
}
