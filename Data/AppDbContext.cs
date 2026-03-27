using ExamenUnidadDos.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamenUnidadDos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmpleadoEntity> Empleados { get; set; }
        public DbSet<PlanillaEntity> Planillas { get; set; }
        public DbSet<DetallePlanillaEntity> DetallesPlanilla { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmpleadoEntity>()
                .HasIndex(e => e.Documento)
                .IsUnique();

            modelBuilder.Entity<PlanillaEntity>()
                .HasIndex(p => p.Periodo)
                .IsUnique();

            modelBuilder.Entity<DetallePlanillaEntity>()
                .HasOne(d => d.Planilla)
                .WithMany(p => p.DetallesPlanilla)
                .HasForeignKey(d => d.PlanillaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DetallePlanillaEntity>()
                .HasOne(d => d.Empleado)
                .WithMany(e => e.DetallesPlanilla)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}