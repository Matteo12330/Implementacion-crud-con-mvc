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
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Tendencia> Tendencias { get; set; }
    }
}
