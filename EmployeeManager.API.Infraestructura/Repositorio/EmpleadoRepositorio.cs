using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.EmployeeManager.API.Infraestructura.Repositorio
{
    
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    { 
        private readonly AppDbContext _context;

        public EmpleadoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Empleado> CrearEmpleadoAsync(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<Empleado> ObtenerEmpleadoPorIdAsync(int id)
        {
            return await _context.Empleados
                .Include(e => e.Horarios)
                .Include(e => e.SolicitudesVacaciones)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Empleado>> ObtenerTodosLosEmpleadosAsync()
        {
            return await _context.Empleados
                .Include(e => e.Horarios)
                .Include(e => e.SolicitudesVacaciones)
                .ToListAsync();
        }

        public async Task<bool> EliminarEmpleadoAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return false;

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Empleado> ObtenerPorUsuarioAsync(string usuario)
        {
            return await _context.Empleados.FirstOrDefaultAsync(e => e.Usuario == usuario);
        }

        public async Task<bool> ActualizarEmpleadoAsync(Empleado empleado)
        {
            try
            {
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false; // Maneja errores adecuadamente
            }
        }

        

    }

       
}
