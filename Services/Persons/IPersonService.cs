using ExamenUnidadDos.Dtos.Persons;

namespace ExamenUnidadDos.Services.Persons
{
    public interface IPersonService
    {
        Task<ServiceResponse> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ServiceResponse> GetOneByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(PersonCreateDto dto);
        Task<ServiceResponse> EditAsync(int id, PersonEditDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}