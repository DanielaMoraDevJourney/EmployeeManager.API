using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
//using EmployeeManager.API.EmployeeManager.API.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.EmployeeManager.API.Infraestructura.Repositorio
{
    
    
    public class SolicitudVacacionesRepositorio : ISolicitudVacacionesRepositorio
    {
        private readonly AppDbContext _context;

        public SolicitudVacacionesRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SolicitudVacaciones> CrearSolicitudAsync(SolicitudVacaciones solicitud)
        {
            _context.SolicitudesVacaciones.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task<List<SolicitudVacaciones>> ObtenerSolicitudesPorEmpleadoIdAsync(int empleadoId)
        {
            return await _context.SolicitudesVacaciones
                .Where(s => s.EmpleadoId == empleadoId)
                .ToListAsync();
        }

        public async Task<SolicitudVacaciones> ActualizarEstadoSolicitudAsync(int id, string estado)
        {
            var solicitud = await _context.SolicitudesVacaciones.FindAsync(id);
            if (solicitud == null) return null;

            solicitud.Estado = estado;
            await _context.SaveChangesAsync();
            return solicitud;
        }
    }
    
}
