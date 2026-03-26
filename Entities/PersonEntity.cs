using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenUnidadDos.Entities
{
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(13)]
        [Column("dni")]
        public string DNI { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [StringLength(10)]
        [Column("gender")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Column("country_id")]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual CountryEntity Country { get; set; } = null!;
    }
}