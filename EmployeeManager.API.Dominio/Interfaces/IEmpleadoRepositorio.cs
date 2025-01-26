using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

namespace EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces
{
    public interface IEmpleadoRepositorio
    {
        Task<Empleado> CrearEmpleadoAsync(Empleado empleado);
        Task<Empleado> ObtenerEmpleadoPorIdAsync(int id);
        Task<List<Empleado>> ObtenerTodosLosEmpleadosAsync();
        Task<bool> EliminarEmpleadoAsync(int id);
        Task<Empleado> ObtenerPorUsuarioAsync(string usuario);
        Task<bool> ActualizarEmpleadoAsync(Empleado empleado);

    }

}
