using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

namespace EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces
{
    public interface ISolicitudVacacionesRepositorio
    {
        Task<SolicitudVacaciones> CrearSolicitudAsync(SolicitudVacaciones solicitud);
        Task<List<SolicitudVacaciones>> ObtenerSolicitudesPorEmpleadoIdAsync(int empleadoId);
        Task<SolicitudVacaciones> ActualizarEstadoSolicitudAsync(int id, string estado); // Aprobar/Rechazar
    }

}
