using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.EmployeeManager.API.Aplicacion.Controladores.Controlador
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudVacacionesController : ControllerBase
    {
        private readonly ISolicitudVacacionesRepositorio _solicitudVacacionesRepositorio;

        public SolicitudVacacionesController(ISolicitudVacacionesRepositorio solicitudVacacionesRepositorio)
        {
            _solicitudVacacionesRepositorio = solicitudVacacionesRepositorio;
        }

        // Crear una solicitud de vacaciones
        [HttpPost]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudVacacionesDto solicitudDto)
        {
            if (solicitudDto == null || solicitudDto.EmpleadoId <= 0)
                return BadRequest("Debe especificar un empleado válido y los detalles de la solicitud.");

            // Mapear el DTO al modelo
            var solicitud = new SolicitudVacaciones
            {
                EmpleadoId = solicitudDto.EmpleadoId,
                FechaSolicitud = solicitudDto.FechaSolicitud,
                FechaInicio = solicitudDto.FechaInicio,
                FechaFin = solicitudDto.FechaFin,
                Estado = solicitudDto.Estado
            };

            var nuevaSolicitud = await _solicitudVacacionesRepositorio.CrearSolicitudAsync(solicitud);

            // Mapear el modelo al DTO de respuesta
            var solicitudResponse = new SolicitudVacacionesDto
            {
                Id = nuevaSolicitud.Id,
                EmpleadoId = nuevaSolicitud.EmpleadoId,
                FechaSolicitud = nuevaSolicitud.FechaSolicitud,
                FechaInicio = nuevaSolicitud.FechaInicio,
                FechaFin = nuevaSolicitud.FechaFin,
                Estado = nuevaSolicitud.Estado
            };

            return CreatedAtAction(nameof(ObtenerSolicitudesPorEmpleadoId), new { empleadoId = solicitudResponse.EmpleadoId }, solicitudResponse);
        }


        // Obtener todas las solicitudes de vacaciones de un empleado
        [HttpGet("empleado/{empleadoId}")]
        public async Task<IActionResult> ObtenerSolicitudesPorEmpleadoId(int empleadoId)
        {
            var solicitudes = await _solicitudVacacionesRepositorio.ObtenerSolicitudesPorEmpleadoIdAsync(empleadoId);
            if (solicitudes == null || solicitudes.Count == 0)
                return NotFound($"No se encontraron solicitudes de vacaciones para el empleado con ID {empleadoId}.");

            return Ok(solicitudes);
        }

        // Aprobar o rechazar una solicitud de vacaciones
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoSolicitud(int id, [FromBody] string estado)
        {
            if (string.IsNullOrWhiteSpace(estado)) return BadRequest("El estado no puede estar vacío.");

            var solicitudActualizada = await _solicitudVacacionesRepositorio.ActualizarEstadoSolicitudAsync(id, estado);
            if (solicitudActualizada == null)
                return NotFound($"No se encontró una solicitud con ID {id}.");

            return Ok(solicitudActualizada);
        }
    }
}
