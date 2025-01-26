namespace EmployeeManager.API.EmployeeManager.API.Dominio.DTOs
{
    public class HorarioDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DiaSemana { get; set; }
        public string Horas { get; set; }
    }

}
