using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
//using EmployeeManager.API.EmployeeManager.API.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.EmployeeManager.API.Infraestructura.Repositorio
{
    
    public class HorarioRepositorio : IHorarioRepositorio
    {
        private readonly AppDbContext _context;

        public HorarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Horario> CrearHorarioAsync(Horario horario)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return horario;
        }

        public async Task<List<Horario>> ObtenerHorariosPorEmpleadoIdAsync(int empleadoId)
        {
            return await _context.Horarios
                .Where(h => h.EmpleadoId == empleadoId)
                .ToListAsync();
        }

        public async Task<bool> EliminarHorarioAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null) return false;

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    

}
