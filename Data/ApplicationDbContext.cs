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

            // Evita error de múltiples rutas en cascada entre Tendencia y Categoría
            modelBuilder.Entity<Tendencia>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tendencias)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Valor por defecto para indicar si una tendencia es "favorita"
            modelBuilder.Entity<Tendencia>()
                .Property(t => t.EsFavorita)
                .HasDefaultValue(false);

            // Especificar tipo compatible con PostgreSQL para FechaCreacion
            modelBuilder.Entity<Tendencia>()
                .Property(t => t.FechaCreacion)
                .HasColumnType("timestamp");

            // Especificamos precisión del campo Precio (10 dígitos, 2 decimales)
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(10, 2);
        }
    }
}
