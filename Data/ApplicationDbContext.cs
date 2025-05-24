using BiteSpot.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteSpot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tendencia> Tendencias { get; set; }
        public DbSet<Opinion> Opiniones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evita error de múltiples rutas en cascada
            modelBuilder.Entity<Tendencia>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tendencias)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura el valor por defecto para EsFavorita
            modelBuilder.Entity<Tendencia>()
                .Property(t => t.EsFavorita)
                .HasDefaultValue(false);
        }
    }
}
