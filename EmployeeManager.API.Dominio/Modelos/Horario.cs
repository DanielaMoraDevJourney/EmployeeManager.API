namespace EmployeeManager.API.EmployeeManager.API.Dominio.Modelos
{
    public class Horario
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DiaSemana { get; set; } // Lunes, Martes, etc.
        public string Horas { get; set; } // Ejemplo: "08:00-16:00"
    }

}
