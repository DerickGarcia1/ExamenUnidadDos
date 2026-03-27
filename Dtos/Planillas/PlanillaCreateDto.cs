using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Dtos.Planillas
{
    public class PlanillaCreateDto
    {
        [Required]
        public string Periodo { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaPago { get; set; }

        public string Estado { get; set; } = "Pendiente";
    }
}