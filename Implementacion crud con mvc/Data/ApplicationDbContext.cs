using Implementacion_crud_con_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Implementacion_crud_con_mvc.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evita error multiple cascade paths
            modelBuilder.Entity<Tendencia>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tendencias)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
