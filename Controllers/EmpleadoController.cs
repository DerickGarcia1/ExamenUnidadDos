using ExamenUnidadDos.Dtos.Empleados;
using ExamenUnidadDos.Services.Empleados;
using Microsoft.AspNetCore.Mvc;

namespace ExamenUnidadDos.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _empleadoService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("activos")]
        public async Task<ActionResult> GetActivos()
        {
            var result = await _empleadoService.GetActivosAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _empleadoService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmpleadoCreateDto dto)
        {
            var result = await _empleadoService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EmpleadoEditDto dto)
        {
            var result = await _empleadoService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _empleadoService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}