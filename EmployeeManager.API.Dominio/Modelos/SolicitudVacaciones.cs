namespace EmployeeManager.API.EmployeeManager.API.Dominio.Modelos
{
    public class SolicitudVacaciones
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } // Pendiente, Aprobada, Rechazada
    }

}
