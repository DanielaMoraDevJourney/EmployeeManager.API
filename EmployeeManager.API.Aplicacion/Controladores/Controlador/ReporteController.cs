using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.EmployeeManager.API.Aplicacion.Controladores.Controlador
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepositorio _reporteRepositorio;

        public ReporteController(IReporteRepositorio reporteRepositorio)
        {
            _reporteRepositorio = reporteRepositorio;
        }

        // Endpoint para generar el reporte comparativo
        [HttpGet("{empleadoId}")]
        public async Task<IActionResult> GenerarReporte(int empleadoId)
        {
            var reporte = await _reporteRepositorio.GenerarReporteComparativoAsync(empleadoId);

            if (reporte == null || !reporte.Any())
                return NotFound($"No se encontraron horarios ni formularios para el empleado con ID {empleadoId}.");

            return Ok(reporte);
        }
    }

}
