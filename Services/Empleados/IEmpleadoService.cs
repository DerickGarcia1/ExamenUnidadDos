using ExamenUnidadDos.Dtos.Empleados;

namespace ExamenUnidadDos.Services.Empleados
{
    public interface IEmpleadoService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetActivosAsync();
        Task<ServiceResponse> GetOneByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(EmpleadoCreateDto dto);
        Task<ServiceResponse> EditAsync(int id, EmpleadoEditDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}