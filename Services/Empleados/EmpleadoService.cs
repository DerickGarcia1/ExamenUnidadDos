using ExamenUnidadDos.Data;
using ExamenUnidadDos.Dtos.Empleados;
using ExamenUnidadDos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenUnidadDos.Services.Empleados
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly AppDbContext _context;

        public EmpleadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var empleados = await _context.Empleados.ToListAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Data = empleados
            };
        }

        public async Task<ServiceResponse> GetActivosAsync()
        {
            var empleados = await _context.Empleados
                .Where(e => e.Activo)
                .ToListAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Data = empleados
            };
        }

        public async Task<ServiceResponse> GetOneByIdAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            return new ServiceResponse
            {
                StatusCode = 200,
                Data = empleado
            };
        }

        public async Task<ServiceResponse> CreateAsync(EmpleadoCreateDto dto)
        {
            var existe = await _context.Empleados
                .AnyAsync(e => e.Documento == dto.Documento);

            if (existe)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "El documento ya existe"
                };
            }

            var empleado = new EmpleadoEntity
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Documento = dto.Documento,
                FechaContratacion = dto.FechaContratacion,
                Departamento = dto.Departamento,
                PuestoTrabajo = dto.PuestoTrabajo,
                SalarioBase = dto.SalarioBase,
                Activo = true
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 201,
                Message = "Empleado creado",
                Data = empleado
            };
        }

        public async Task<ServiceResponse> EditAsync(int id, EmpleadoEditDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            var existeDocumento = await _context.Empleados
                .AnyAsync(e => e.Documento == dto.Documento && e.Id != id);

            if (existeDocumento)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "El documento ya está en uso"
                };
            }

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Documento = dto.Documento;
            empleado.FechaContratacion = dto.FechaContratacion;
            empleado.Departamento = dto.Departamento;
            empleado.PuestoTrabajo = dto.PuestoTrabajo;
            empleado.SalarioBase = dto.SalarioBase;
            empleado.Activo = dto.Activo;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Empleado actualizado",
                Data = empleado
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            empleado.Activo = false;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Empleado desactivado"
            };
        }
    }
}