using ExamenUnidadDos.Data;
using ExamenUnidadDos.Dtos.Planillas;
using ExamenUnidadDos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenUnidadDos.Services.Planillas
{
    public class PlanillaService : IPlanillaService
    {
        private readonly AppDbContext _context;

        public PlanillaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var planillas = await _context.Planillas
                .Include(p => p.DetallesPlanilla)
                .ToListAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = planillas
            };
        }

        public async Task<ServiceResponse> GetOneByIdAsync(int id)
        {
            var planilla = await _context.Planillas
                .Include(p => p.DetallesPlanilla)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (planilla == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = planilla
            };
        }

        public async Task<ServiceResponse> GetByPeriodoAsync(string periodo)
        {
            var planilla = await _context.Planillas
                .Include(p => p.DetallesPlanilla)
                .FirstOrDefaultAsync(p => p.Periodo == periodo);

            if (planilla == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "No se encontró planilla para ese período"
                };
            }

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Consulta exitosa",
                Data = planilla
            };
        }

        public async Task<ServiceResponse> CreateAsync(PlanillaCreateDto dto)
        {
            var existePeriodo = await _context.Planillas
                .AnyAsync(p => p.Periodo == dto.Periodo);

            if (existePeriodo)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Ya existe una planilla para ese período"
                };
            }

            var estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Pendiente" : dto.Estado;

            var planilla = new PlanillaEntity
            {
                Periodo = dto.Periodo,
                FechaCreacion = dto.FechaCreacion == default ? DateTime.Now : dto.FechaCreacion,
                FechaPago = dto.FechaPago == default ? DateTime.Now : dto.FechaPago,
                Estado = estado
            };

            _context.Planillas.Add(planilla);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 201,
                Message = "Planilla creada correctamente",
                Data = planilla
            };
        }

        public async Task<ServiceResponse> EditAsync(int id, PlanillaEditDto dto)
        {
            var planilla = await _context.Planillas.FindAsync(id);

            if (planilla == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            var existePeriodo = await _context.Planillas
                .AnyAsync(p => p.Periodo == dto.Periodo && p.Id != id);

            if (existePeriodo)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Ya existe otra planilla para ese período"
                };
            }

            planilla.Periodo = dto.Periodo;
            planilla.FechaCreacion = dto.FechaCreacion;
            planilla.FechaPago = dto.FechaPago;
            planilla.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Planilla actualizada correctamente",
                Data = planilla
            };
        }

        public async Task<ServiceResponse> UpdateEstadoAsync(int id, PlanillaEstadoDto dto)
        {
            var planilla = await _context.Planillas.FindAsync(id);

            if (planilla == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            if (string.IsNullOrWhiteSpace(dto.Estado))
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "El estado es obligatorio"
                };
            }

            var estadosValidos = new[] { "Pendiente", "Pagada", "Anulada" };

            if (!estadosValidos.Contains(dto.Estado))
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Estado inválido. Use: Pendiente, Pagada o Anulada"
                };
            }

            planilla.Estado = dto.Estado;
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Estado actualizado correctamente",
                Data = planilla
            };
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);

            if (planilla == null)
            {
                return new ServiceResponse
                {
                    StatusCode = 404,
                    Message = "Planilla no encontrada"
                };
            }

            if (planilla.Estado == "Pagada")
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "No se puede eliminar una planilla pagada"
                };
            }

            _context.Planillas.Remove(planilla);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                StatusCode = 200,
                Message = "Planilla eliminada correctamente"
            };
        }

        public async Task<ServiceResponse> GenerarAsync()
        {
            var periodoActual = $"{DateTime.Now:yyyy-MM}";

            var existePeriodo = await _context.Planillas
                .AnyAsync(p => p.Periodo == periodoActual);

            if (existePeriodo)
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "Ya existe una planilla para el período actual"
                };
            }

            var empleadosActivos = await _context.Empleados
                .Where(e => e.Activo)
                .ToListAsync();

            if (!empleadosActivos.Any())
            {
                return new ServiceResponse
                {
                    StatusCode = 400,
                    Message = "No hay empleados activos para generar la planilla"
                };
            }

            var planilla = new PlanillaEntity
            {
                Periodo = periodoActual,
                FechaCreacion = DateTime.Now,
                FechaPago = DateTime.Now,
                Estado = "Pendiente"
            };

            _context.Planillas.Add(planilla);
            await _context.SaveChangesAsync();

            var detalles = empleadosActivos.Select(e => new DetallePlanillaEntity
            {
                PlanillaId = planilla.Id,
                EmpleadoId = e.Id,
                SalarioBase = e.SalarioBase,
                HorasExtra = 0,
                MontoHorasExtra = 0,
                Bonificaciones = 0,
                Deducciones = 0,
                SalarioNeto = e.SalarioBase,
                Comentarios = "Generado automáticamente"
            }).ToList();

            _context.DetallesPlanilla.AddRange(detalles);
            await _context.SaveChangesAsync();

            var planillaCompleta = await _context.Planillas
                .Include(p => p.DetallesPlanilla)
                .FirstOrDefaultAsync(p => p.Id == planilla.Id);

            return new ServiceResponse
            {
                StatusCode = 201,
                Message = "Planilla generada correctamente",
                Data = planillaCompleta
            };
        }
    }
}