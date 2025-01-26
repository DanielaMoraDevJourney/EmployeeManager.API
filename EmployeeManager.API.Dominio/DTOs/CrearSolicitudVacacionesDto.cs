namespace EmployeeManager.API.EmployeeManager.API.Dominio.DTOs
{
    public class CrearSolicitudVacacionesDto
    {
        public int EmpleadoId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Default: Pendiente
    }


}
