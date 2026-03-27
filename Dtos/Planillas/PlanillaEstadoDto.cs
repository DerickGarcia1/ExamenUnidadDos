using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Dtos.Planillas
{
    public class PlanillaEstadoDto
    {
        [Required]
        public string Estado { get; set; } = string.Empty;
    }
}