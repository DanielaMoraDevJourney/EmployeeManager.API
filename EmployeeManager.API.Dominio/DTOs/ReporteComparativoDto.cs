namespace EmployeeManager.API.EmployeeManager.API.Dominio.DTOs
{
    public class ReporteComparativoDto
    {
        public int HorarioId { get; set; }
        public DateTime FechaInicioHorario { get; set; }
        public DateTime FechaFinHorario { get; set; }
        public string HorasAsignadas { get; set; }
        public bool LlenoFormulario { get; set; }
        public DateTime? FechaFormulario { get; set; }
        public string NombreEmpleado { get; set; }
        public string UsuarioEmpleado { get; set; }
    }

}
