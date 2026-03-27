using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenUnidadDos.Entities
{
    public class PlanillaEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("periodo")]
        public string Periodo { get; set; } = string.Empty;

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; }

        [StringLength(20)]
        [Column("estado")]
        public string Estado { get; set; } = string.Empty; // Pendiente, Pagada, Anulada

        public ICollection<DetallePlanillaEntity> DetallesPlanilla { get; set; } = new List<DetallePlanillaEntity>();
    }
}