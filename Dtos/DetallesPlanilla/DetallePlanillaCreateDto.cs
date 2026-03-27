using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Dtos.DetallesPlanilla
{
    public class DetallePlanillaCreateDto
    {
        [Required]
        public int PlanillaId { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SalarioBase { get; set; }

        [Range(0, double.MaxValue)]
        public decimal HorasExtra { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MontoHorasExtra { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Bonificaciones { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Deducciones { get; set; }

        public string Comentarios { get; set; } = string.Empty;
    }
}