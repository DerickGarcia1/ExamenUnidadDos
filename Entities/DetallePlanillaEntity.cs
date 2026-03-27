using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenUnidadDos.Entities
{
    public class DetallePlanillaEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("planilla_id")]
        public int PlanillaId { get; set; }

        [Required]
        [Column("empleado_id")]
        public int EmpleadoId { get; set; }

        [Column("salario_base", TypeName = "TEXT")]
        public decimal SalarioBase { get; set; }

        [Column("horas_extra", TypeName = "TEXT")]
        public decimal HorasExtra { get; set; }

        [Column("monto_horas_extra", TypeName = "TEXT")]
        public decimal MontoHorasExtra { get; set; }

        [Column("bonificaciones", TypeName = "TEXT")]
        public decimal Bonificaciones { get; set; }

        [Column("deducciones", TypeName = "TEXT")]
        public decimal Deducciones { get; set; }

        [Column("salario_neto", TypeName = "TEXT")]
        public decimal SalarioNeto { get; set; }

        [Column("comentarios")]
        public string Comentarios { get; set; } = string.Empty;

        [ForeignKey(nameof(PlanillaId))]
        public virtual PlanillaEntity Planilla { get; set; } = null!;

        [ForeignKey(nameof(EmpleadoId))]
        public virtual EmpleadoEntity Empleado { get; set; } = null!;
    }
}