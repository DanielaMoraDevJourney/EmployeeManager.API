using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.EmployeeManager.API.Aplicacion.Controladores.Controlador
{


    [ApiController]
    [Route("api/[controller]")]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioRepositorio _horarioRepositorio;

        public HorarioController(IHorarioRepositorio horarioRepositorio)
        {
            _horarioRepositorio = horarioRepositorio;
        }

        // Crear un horario
        [HttpPost]
        public async Task<IActionResult> CrearHorario([FromBody] CrearHorarioDto horarioDto)
        {
            if (horarioDto == null || horarioDto.EmpleadoId <= 0)
                return BadRequest("Debe especificar un empleado válido.");

            var horario = new Horario
            {
                EmpleadoId = horarioDto.EmpleadoId,
                FechaInicio = horarioDto.FechaInicio,
                FechaFin = horarioDto.FechaFin,
                DiaSemana = horarioDto.DiaSemana,
                Horas = horarioDto.Horas
            };

            var nuevoHorario = await _horarioRepositorio.CrearHorarioAsync(horario);
            return CreatedAtAction(nameof(ObtenerHorariosPorEmpleadoId), new { empleadoId = nuevoHorario.EmpleadoId }, nuevoHorario);
        }



        // Obtener los horarios de un empleado por su ID
        [HttpGet("empleado/{empleadoId}")]
        public async Task<IActionResult> ObtenerHorariosPorEmpleadoId(int empleadoId)
        {
            var horarios = await _horarioRepositorio.ObtenerHorariosPorEmpleadoIdAsync(empleadoId);
            if (horarios == null || horarios.Count == 0)
                return NotFound($"No se encontraron horarios para el empleado con ID {empleadoId}.");

            return Ok(horarios);
        }

        // Eliminar un horario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHorario(int id)
        {
            var eliminado = await _horarioRepositorio.EliminarHorarioAsync(id);
            if (!eliminado) return NotFound($"No se pudo eliminar el horario con ID {id}. Es posible que no exista.");

            return NoContent();
        }
    }

}
