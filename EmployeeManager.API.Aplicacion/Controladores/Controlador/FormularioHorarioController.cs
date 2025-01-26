using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.EmployeeManager.API.Aplicacion.Controladores.Controlador
{
    using global::EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class FormularioHorarioController : ControllerBase
    {
        private readonly IFormularioHorarioRepositorio _formularioHorarioRepositorio;

        public FormularioHorarioController(IFormularioHorarioRepositorio formularioHorarioRepositorio)
        {
            _formularioHorarioRepositorio = formularioHorarioRepositorio;
        }

        // Registrar un formulario
        [HttpPost]
        public async Task<IActionResult> RegistrarFormulario([FromBody] CrearFormularioHorarioDto formularioDto)
        {
            if (formularioDto == null || formularioDto.EmpleadoId <= 0)
                return BadRequest("Debe especificar un empleado válido y datos del formulario.");

            var formulario = new FormularioHorario
            {
                EmpleadoId = formularioDto.EmpleadoId,
                Fecha = formularioDto.Fecha,
                Llenado = formularioDto.Llenado
            };

            var nuevoFormulario = await _formularioHorarioRepositorio.RegistrarFormularioAsync(formulario);
            return CreatedAtAction(nameof(ObtenerFormulariosPorEmpleadoId), new { empleadoId = nuevoFormulario.EmpleadoId }, nuevoFormulario);
        }

        // Obtener formularios por empleado
        [HttpGet("empleado/{empleadoId}")]
        public async Task<IActionResult> ObtenerFormulariosPorEmpleadoId(int empleadoId)
        {
            var formularios = await _formularioHorarioRepositorio.ObtenerFormulariosPorEmpleadoIdAsync(empleadoId);
            if (formularios == null || formularios.Count == 0)
                return NotFound($"No se encontraron formularios para el empleado con ID {empleadoId}.");

            return Ok(formularios);
        }

        // Actualizar el estado del formulario
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoFormulario(int id, [FromBody] ActualizarEstadoFormularioDto estadoDto)
        {
            if (estadoDto == null)
                return BadRequest("Debe proporcionar el estado del formulario.");

            var actualizado = await _formularioHorarioRepositorio.ActualizarEstadoFormularioAsync(id, estadoDto.Llenado);
            if (!actualizado)
                return NotFound($"No se encontró un formulario con ID {id}.");

            return Ok(new { mensaje = "Estado del formulario actualizado exitosamente.", llenado = estadoDto.Llenado });
        }
    }

}
