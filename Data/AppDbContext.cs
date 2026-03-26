using Microsoft.EntityFrameworkCore;
using ExamenUnidadDos.Entities;

namespace ExamenUnidadDos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<CountryEntity> Countries { get; set; }
    }
}