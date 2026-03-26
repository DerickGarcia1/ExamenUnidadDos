using System.ComponentModel.DataAnnotations;

namespace ExamenUnidadDos.Entities
{
    public class CountryEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
    }
}