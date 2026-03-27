using ExamenUnidadDos.Dtos.DetallesPlanilla;

namespace ExamenUnidadDos.Services.DetallesPlanilla
{
    public interface IDetallePlanillaService
    {
        Task<ServiceResponse> GetByPlanillaAsync(int planillaId);
        Task<ServiceResponse> GetByEmpleadoAsync(int empleadoId);
        Task<ServiceResponse> GetOneByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(DetallePlanillaCreateDto dto);
        Task<ServiceResponse> EditAsync(int id, DetallePlanillaEditDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}