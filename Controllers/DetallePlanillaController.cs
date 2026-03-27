using ExamenUnidadDos.Dtos.DetallesPlanilla;
using ExamenUnidadDos.Services.DetallesPlanilla;
using Microsoft.AspNetCore.Mvc;

namespace ExamenUnidadDos.Controllers
{
    [Route("api/detalles-planilla")]
    [ApiController]
    public class DetallePlanillaController : ControllerBase
    {
        private readonly IDetallePlanillaService _detallePlanillaService;

        public DetallePlanillaController(IDetallePlanillaService detallePlanillaService)
        {
            _detallePlanillaService = detallePlanillaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _detallePlanillaService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("planilla/{planillaId}")]
        public async Task<ActionResult> GetByPlanilla(int planillaId)
        {
            var result = await _detallePlanillaService.GetByPlanillaAsync(planillaId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<ActionResult> GetByEmpleado(int empleadoId)
        {
            var result = await _detallePlanillaService.GetByEmpleadoAsync(empleadoId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DetallePlanillaCreateDto dto)
        {
            var result = await _detallePlanillaService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DetallePlanillaEditDto dto)
        {
            var result = await _detallePlanillaService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _detallePlanillaService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}