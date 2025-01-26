using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;

namespace EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces
{
    public interface IReporteRepositorio
    {
        Task<List<ReporteComparativoDto>> GenerarReporteComparativoAsync(int empleadoId);
    }
}
