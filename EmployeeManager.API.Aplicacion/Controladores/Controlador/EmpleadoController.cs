using EmployeeManager.API.EmployeeManager.API.Dominio.DTOs;
using EmployeeManager.API.EmployeeManager.API.Dominio.Interfaces;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.API.EmployeeManager.API.Aplicacion.Controladores.Controlador
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepositorio _empleadoRepositorio;

        public EmpleadoController(IEmpleadoRepositorio empleadoRepositorio)
        {
            _empleadoRepositorio = empleadoRepositorio;
        }

        // Crear un nuevo empleado
        [HttpPost]
        public async Task<IActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            if (empleado == null) return BadRequest("El empleado no puede ser nulo.");

            var nuevoEmpleado = await _empleadoRepositorio.CrearEmpleadoAsync(empleado);
            return CreatedAtAction(nameof(ObtenerEmpleadoPorId), new { id = nuevoEmpleado.Id }, nuevoEmpleado);
        }

        // Obtener un empleado por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEmpleadoPorId(int id)
        {
            var empleado = await _empleadoRepositorio.ObtenerEmpleadoPorIdAsync(id);
            if (empleado == null)
                return NotFound($"No se encontró un empleado con ID {id}.");

            // Mapear a DTO
            var empleadoDto = new EmpleadoDto
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Usuario = empleado.Usuario,
                Horarios = empleado.Horarios.Select(h => new HorarioDto
                {
                    Id = h.Id,
                    FechaInicio = h.FechaInicio,
                    FechaFin = h.FechaFin,
                    DiaSemana = h.DiaSemana,
                    Horas = h.Horas
                }).ToList(),
                SolicitudesVacaciones = empleado.SolicitudesVacaciones.Select(s => new SolicitudVacacionesDto
                {
                    Id = s.Id,
                    EmpleadoId = s.EmpleadoId,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaInicio = s.FechaInicio,
                    FechaFin = s.FechaFin,
                    Estado = s.Estado
                }).ToList()
            };

            return Ok(empleadoDto);
        }



        // Listar todos los empleados
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosEmpleados()
        {
            var empleados = await _empleadoRepositorio.ObtenerTodosLosEmpleadosAsync();

            var empleadosDto = empleados.Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Usuario = e.Usuario,
                Horarios = e.Horarios.Select(h => new HorarioDto
                {
                    Id = h.Id,
                    FechaInicio = h.FechaInicio,
                    FechaFin = h.FechaFin,
                    DiaSemana = h.DiaSemana,
                    Horas = h.Horas
                }).ToList(),
                SolicitudesVacaciones = e.SolicitudesVacaciones.Select(s => new SolicitudVacacionesDto
                {
                    Id = s.Id,
                    EmpleadoId = s.EmpleadoId,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaInicio = s.FechaInicio,
                    FechaFin = s.FechaFin,
                    Estado = s.Estado
                }).ToList()
            }).ToList();

            return Ok(empleadosDto);
        }



        // Eliminar un empleado
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            var eliminado = await _empleadoRepositorio.EliminarEmpleadoAsync(id);
            if (!eliminado) return NotFound($"No se pudo eliminar el empleado con ID {id}. Es posible que no exista.");

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var empleado = await _empleadoRepositorio.ObtenerPorUsuarioAsync(loginDto.Usuario);

            if (empleado == null || empleado.Contraseña != loginDto.Contrasena)
            {
                return Unauthorized(new { Mensaje = "Usuario o contraseña incorrectos." });
            }

            return Ok(new
            {
                empleado.Id,
                empleado.Nombre,
                empleado.Usuario
            });
        }

        // Editar un empleado existente
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarEmpleado(int id, [FromBody] EditarEmpleadoDto empleadoDto)
        {
            if (empleadoDto == null)
            {
                return BadRequest("Los datos del empleado son inválidos.");
            }

            var empleadoExistente = await _empleadoRepositorio.ObtenerEmpleadoPorIdAsync(id);

            if (empleadoExistente == null)
            {
                return NotFound($"No se encontró un empleado con el ID {id}.");
            }

            // Actualizar los campos permitidos del empleado
            empleadoExistente.Nombre = empleadoDto.Nombre;
            empleadoExistente.Usuario = empleadoDto.Usuario;

            // Solo actualizar la contraseña si se proporciona
            if (!string.IsNullOrEmpty(empleadoDto.Contraseña))
            {
                empleadoExistente.Contraseña = empleadoDto.Contraseña;
            }

            var actualizado = await _empleadoRepositorio.ActualizarEmpleadoAsync(empleadoExistente);

            if (!actualizado)
            {
                return StatusCode(500, "Ocurrió un error al intentar actualizar el empleado. Intente nuevamente.");
            }

            return NoContent(); // Retorna 204 si la actualización es exitosa
        }





    }

}
