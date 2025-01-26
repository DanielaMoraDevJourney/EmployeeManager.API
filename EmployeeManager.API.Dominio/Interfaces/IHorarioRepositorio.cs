using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

namespace EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces
{
    public interface IHorarioRepositorio
    {
        Task<Horario> CrearHorarioAsync(Horario horario);
        Task<List<Horario>> ObtenerHorariosPorEmpleadoIdAsync(int empleadoId);
        Task<bool> EliminarHorarioAsync(int id);
    }

}
