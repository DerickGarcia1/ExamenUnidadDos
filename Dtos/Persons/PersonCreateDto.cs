using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Dtos.Persons
{
    public class PersonCreateDto
    {
        [Required]
        [StringLength(13)]
        public string DNI { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public int CountryId { get; set; }
    }
}