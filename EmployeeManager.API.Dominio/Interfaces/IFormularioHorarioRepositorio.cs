using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

namespace EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces
{
    public interface IFormularioHorarioRepositorio
    {
        Task<FormularioHorario> RegistrarFormularioAsync(FormularioHorario formulario);
        Task<List<FormularioHorario>> ObtenerFormulariosPorEmpleadoIdAsync(int empleadoId);
        Task<bool> ActualizarEstadoFormularioAsync(int id, bool llenado);
    }

}
