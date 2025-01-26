using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.EmployeeManager.API.Infraestructura.Repositorio
{
    
    public class ReporteRepositorio : IReporteRepositorio
    {
        private readonly AppDbContext _context;

        public ReporteRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteComparativoDto>> GenerarReporteComparativoAsync(int empleadoId)
        {
            // Consultar el empleado
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(e => e.Id == empleadoId);

            if (empleado == null)
                return new List<ReporteComparativoDto>(); // Retorna vacío si no existe el empleado

            // Consultar los horarios del empleado
            var horarios = await _context.Horarios
                .Where(h => h.EmpleadoId == empleadoId)
                .ToListAsync();

            // Consultar los formularios del empleado
            var formularios = await _context.FormulariosHorarios
                .Where(f => f.EmpleadoId == empleadoId)
                .ToListAsync();

            // Generar el reporte comparativo
            var reporte = horarios.Select(h => new ReporteComparativoDto
            {
                HorarioId = h.Id,
                FechaInicioHorario = h.FechaInicio,
                FechaFinHorario = h.FechaFin,
                HorasAsignadas = h.Horas,
                LlenoFormulario = formularios.Any(f => f.Fecha.Date == h.FechaInicio.Date && f.Llenado),
                FechaFormulario = formularios.FirstOrDefault(f => f.Fecha.Date == h.FechaInicio.Date)?.Fecha,
                NombreEmpleado = empleado.Nombre,
                UsuarioEmpleado = empleado.Usuario
            }).ToList();

            return reporte;
        }


    }
    
}
