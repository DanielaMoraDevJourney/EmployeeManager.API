namespace EmployeeManager.API.EmployeeManager.API.Dominio.Modelos
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public ICollection<Horario> Horarios { get; set; } = new List<Horario>();
        public ICollection<SolicitudVacaciones> SolicitudesVacaciones { get; set; } = new List<SolicitudVacaciones>();
    }
}
