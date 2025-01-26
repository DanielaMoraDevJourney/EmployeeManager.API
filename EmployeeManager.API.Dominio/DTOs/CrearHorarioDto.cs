namespace EmployeeManager.API.EmployeeManager.API.Dominio.DTOs
{
    public class CrearHorarioDto
    {
        public int EmpleadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DiaSemana { get; set; }
        public string Horas { get; set; }
    }
}
