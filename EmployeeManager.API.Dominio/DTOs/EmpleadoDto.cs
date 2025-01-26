namespace EmployeeManager.API.EmployeeManager.API.Dominio.DTOs
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public List<HorarioDto> Horarios { get; set; }
        public List<SolicitudVacacionesDto> SolicitudesVacaciones { get; set; }
    }

}
