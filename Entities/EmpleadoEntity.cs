using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenUnidadDos.Entities
{
    public class EmpleadoEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("documento")]
        public string Documento { get; set; } = string.Empty;

        [Column("fecha_contratacion")]
        public DateTime FechaContratacion { get; set; }

        [StringLength(100)]
        [Column("departamento")]
        public string Departamento { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("puesto_trabajo")]
        public string PuestoTrabajo { get; set; } = string.Empty;

        [Column("salario_base", TypeName = "TEXT")]
        public decimal SalarioBase { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        public ICollection<DetallePlanillaEntity> DetallesPlanilla { get; set; } = new List<DetallePlanillaEntity>();
    }
}