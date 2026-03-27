using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Dtos.Empleados
{
    public class EmpleadoCreateDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public string Documento { get; set; } = string.Empty;

        public DateTime FechaContratacion { get; set; }

        public string Departamento { get; set; } = string.Empty;

        public string PuestoTrabajo { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal SalarioBase { get; set; }

        public bool Activo { get; set; } = true;
    }
}