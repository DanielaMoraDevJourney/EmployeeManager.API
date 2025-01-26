using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
//using EmployeeManager.API.EmployeeManager.API.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.API.EmployeeManager.API.Infraestructura.Repositorio
{
    
    public class FormularioHorarioRepositorio : IFormularioHorarioRepositorio
    {
        private readonly AppDbContext _context;

        public FormularioHorarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FormularioHorario> RegistrarFormularioAsync(FormularioHorario formulario)
        {
            _context.FormulariosHorarios.Add(formulario);
            await _context.SaveChangesAsync();
            return formulario;
        }

        public async Task<List<FormularioHorario>> ObtenerFormulariosPorEmpleadoIdAsync(int empleadoId)
        {
            return await _context.FormulariosHorarios
                .Where(f => f.EmpleadoId == empleadoId)
                .ToListAsync();
        }

        public async Task<bool> ActualizarEstadoFormularioAsync(int id, bool llenado)
        {
            var formulario = await _context.FormulariosHorarios.FindAsync(id);
            if (formulario == null) return false;

            formulario.Llenado = llenado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
}
