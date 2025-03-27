using Microsoft.EntityFrameworkCore;
using Implementacion_crud_con_mvc.Models; // Ajusta según tu namespace

namespace Implementacion_crud_con_mvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tendencia> Tendencias { get; set; } // Agrega todas tus entidades
        public DbSet<Libro> Libros { get; set; }
    }
}
