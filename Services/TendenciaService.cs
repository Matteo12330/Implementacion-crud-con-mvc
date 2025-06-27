using BiteSpot.Data;
using BiteSpot.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteSpot.Services
{
    public class TendenciaService : ITendenciaService
    {
        private readonly ApplicationDbContext _context;

        public TendenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ActualizarTendenciaFavoritos()
        {
            var tendenciaFavoritos = _context.Tendencias.FirstOrDefault(t => t.EsFavorita);

            if (tendenciaFavoritos == null)
            {
                tendenciaFavoritos = new Tendencia
                {
                    Nombre = "Favoritos de los usuarios",
                    Descripcion = "Productos con mejor calificación promedio",
                    FechaCreacion = DateTime.Now,
                    EsFavorita = true,
                    CategoriaId = _context.Categorias.First().Id
                };

                _context.Tendencias.Add(tendenciaFavoritos);
                _context.SaveChanges();
            }

            var fechaLimite = DateTime.Now.AddDays(-30);

            var mejoresProductos = _context.Productos
                .Include(p => p.Opiniones)
                .ToList()
                .Where(p =>
                    p.Opiniones.Count(o => o.Puntuacion >= 4 && o.Fecha >= fechaLimite) >= 3 &&
                    p.Opiniones.Where(o => o.Fecha >= fechaLimite).Average(o => o.Puntuacion) >= 4.5
                ).ToList();

            var productosExistentes = _context.Productos.Where(p => p.TendenciaId == tendenciaFavoritos.Id).ToList();
            foreach (var p in productosExistentes)
            {
                p.TendenciaId = null;
            }

            foreach (var p in mejoresProductos)
            {
                p.TendenciaId = tendenciaFavoritos.Id;
            }

            _context.SaveChanges();

            bool hayVinculados = _context.Productos.Any(p => p.TendenciaId == tendenciaFavoritos.Id);
            if (!hayVinculados)
            {
                _context.Tendencias.Remove(tendenciaFavoritos);
                _context.SaveChanges();
            }
        }
    }
}