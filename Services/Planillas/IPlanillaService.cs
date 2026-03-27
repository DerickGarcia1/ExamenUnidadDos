using ExamenUnidadDos.Dtos.Planillas;

namespace ExamenUnidadDos.Services.Planillas
{
    public interface IPlanillaService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetOneByIdAsync(int id);
        Task<ServiceResponse> GetByPeriodoAsync(string periodo);
        Task<ServiceResponse> CreateAsync(PlanillaCreateDto dto);
        Task<ServiceResponse> EditAsync(int id, PlanillaEditDto dto);
        Task<ServiceResponse> UpdateEstadoAsync(int id, PlanillaEstadoDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> GenerarAsync();
    }
}