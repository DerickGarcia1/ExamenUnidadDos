using ExamenUnidadDos.Data;
using ExamenUnidadDos.Dtos.DetallesPlanilla;
using ExamenUnidadDos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenUnidadDos.Services.DetallesPlanilla
{
    public class DetallePlanillaService : IDetallePlanillaService
    {
        private readonly AppDbContext _context;

        public DetallePlanillaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> GetByPlanillaAsync(int planillaId)
        {
            var existePlanilla = await _context.Planillas.AnyAsync(p => p.Id == planillaId);

            if (!existePlanilla)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            var detalles = await _context.DetallesPlanilla
                .Include(d => d.Planilla)
                .Include(d => d.Empleado)
                .Where(d => d.PlanillaId == planillaId)
                .ToListAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = detalles
            };
        }

        public async Task<ServiceResponse> GetByEmpleadoAsync(int empleadoId)
        {
            var existeEmpleado = await _context.Empleados.AnyAsync(e => e.Id == empleadoId);

            if (!existeEmpleado)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            var detalles = await _context.DetallesPlanilla
                .Include(d => d.Planilla)
                .Include(d => d.Empleado)
                .Where(d => d.EmpleadoId == empleadoId)
                .ToListAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = detalles
            };
        }

        public async Task<ServiceResponse> GetOneByIdAsync(int id)
        {
            var detalle = await _context.DetallesPlanilla
                .Include(d => d.Planilla)
                .Include(d => d.Empleado)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalle == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Detalle de planilla no encontrado"
                };
            }

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = detalle
            };
        }

        public async Task<ServiceResponse> CreateAsync(DetallePlanillaCreateDto dto)
        {
            if (dto.SalarioBase < 0 || dto.HorasExtra < 0 || dto.MontoHorasExtra < 0 ||
                dto.Bonificaciones < 0 || dto.Deducciones < 0)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Los montos no pueden ser negativos"
                };
            }

            var existePlanilla = await _context.Planillas.AnyAsync(p => p.Id == dto.PlanillaId);
            if (!existePlanilla)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            var existeEmpleado = await _context.Empleados.AnyAsync(e => e.Id == dto.EmpleadoId);
            if (!existeEmpleado)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            var salarioNeto = dto.SalarioBase + dto.MontoHorasExtra + dto.Bonificaciones - dto.Deducciones;

            var detalle = new DetallePlanillaEntity
            {
                PlanillaId = dto.PlanillaId,
                EmpleadoId = dto.EmpleadoId,
                SalarioBase = dto.SalarioBase,
                HorasExtra = dto.HorasExtra,
                MontoHorasExtra = dto.MontoHorasExtra,
                Bonificaciones = dto.Bonificaciones,
                Deducciones = dto.Deducciones,
                SalarioNeto = salarioNeto,
                Comentarios = dto.Comentarios
            };

            _context.DetallesPlanilla.Add(detalle);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 201,
                Message = "Detalle de planilla creado correctamente",
                Data = detalle
            };
        }

        public async Task<ServiceResponse> EditAsync(int id, DetallePlanillaEditDto dto)
        {
            var detalle = await _context.DetallesPlanilla.FindAsync(id);

            if (detalle == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Detalle de planilla no encontrado"
                };
            }

            if (dto.SalarioBase < 0 || dto.HorasExtra < 0 || dto.MontoHorasExtra < 0 ||
                dto.Bonificaciones < 0 || dto.Deducciones < 0)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Los montos no pueden ser negativos"
                };
            }

            var existePlanilla = await _context.Planillas.AnyAsync(p => p.Id == dto.PlanillaId);
            if (!existePlanilla)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            var existeEmpleado = await _context.Empleados.AnyAsync(e => e.Id == dto.EmpleadoId);
            if (!existeEmpleado)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Empleado no encontrado"
                };
            }

            detalle.PlanillaId = dto.PlanillaId;
            detalle.EmpleadoId = dto.EmpleadoId;
            detalle.SalarioBase = dto.SalarioBase;
            detalle.HorasExtra = dto.HorasExtra;
            detalle.MontoHorasExtra = dto.MontoHorasExtra;
            detalle.Bonificaciones = dto.Bonificaciones;
            detalle.Deducciones = dto.Deducciones;
            detalle.SalarioNeto = dto.SalarioBase + dto.MontoHorasExtra + dto.Bonificaciones - dto.Deducciones;
            detalle.Comentarios = dto.Comentarios;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Detalle de planilla actualizado correctamente",
                Data = detalle
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var detalle = await _context.DetallesPlanilla.FindAsync(id);

            if (detalle == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Detalle de planilla no encontrado"
                };
            }

            _context.DetallesPlanilla.Remove(detalle);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Detalle de planilla eliminado correctamente"
            };
        }
    }
}