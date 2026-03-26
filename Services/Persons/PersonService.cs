using ExamenUnidadDos.Data;
using ExamenUnidadDos.Dtos.Persons;
using ExamenUnidadDos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenUnidadDos.Services.Persons
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _context;

        public PersonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Persons
                .Include(p => p.Country)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.DNI.Contains(searchTerm) ||
                    p.FirstName.Contains(searchTerm) ||
                    p.LastName.Contains(searchTerm) ||
                    p.Gender.Contains(searchTerm) ||
                    p.Country.Name.Contains(searchTerm));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.Id,
                    p.DNI,
                    p.FirstName,
                    p.LastName,
                    p.BirthDate,
                    p.Gender,
                    p.CountryId,
                    Country = p.Country.Name
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagination = new PaginationResponse<object>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = pagination
            };
        }

        public async Task<ServiceResponse> GetOneByIdAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Country)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Persona no encontrada"
                };
            }

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = new
                {
                    person.Id,
                    person.DNI,
                    person.FirstName,
                    person.LastName,
                    person.BirthDate,
                    person.Gender,
                    person.CountryId,
                    Country = person.Country.Name
                }
            };
        }

        public async Task<ServiceResponse> CreateAsync(PersonCreateDto dto)
        {
            var countryExists = await _context.Countries.AnyAsync(c => c.Id == dto.CountryId);

            if (!countryExists)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "El país no existe"
                };
            }

            var person = new PersonEntity
            {
                DNI = dto.DNI,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                CountryId = dto.CountryId
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 201,
                Message = "Persona creada correctamente",
                Data = person
            };
        }

        public async Task<ServiceResponse> EditAsync(int id, PersonEditDto dto)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Persona no encontrada"
                };
            }

            var countryExists = await _context.Countries.AnyAsync(c => c.Id == dto.CountryId);

            if (!countryExists)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "El país no existe"
                };
            }

            person.DNI = dto.DNI;
            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.BirthDate = dto.BirthDate;
            person.Gender = dto.Gender;
            person.CountryId = dto.CountryId;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Persona actualizada correctamente",
                Data = person
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Persona no encontrada"
                };
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Persona eliminada correctamente"
            };
        }
    }
}