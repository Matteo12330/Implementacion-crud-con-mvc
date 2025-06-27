/*
using BiteSpot.Data;
using BiteSpot.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteSpot.Helpers
{
    // Aqui nos encargamos de actualizar la tendencia especial llamada "Favoritos de los usuarios"
    public static class TendenciaHelper
    {
        // Se llama a este método cada vez que se crea o elimina una opinión
        public static void ActualizarTendenciaFavoritos(ApplicationDbContext context)
        {
            // Paso 1: Verificamos si ya existe una tendencia marcada como favorita
            var tendenciaFavoritos = context.Tendencias.FirstOrDefault(t => t.EsFavorita);

            // Si no existe, se crea automáticamente
            if (tendenciaFavoritos == null)
            {
                tendenciaFavoritos = new Tendencia
                {
                    Nombre = "Favoritos de los usuarios",
                    Descripcion = "Productos con mejor calificación promedio",
                    FechaCreacion = DateTime.Now, // ✅ Compatibilidad con timestamp without time zone
                    EsFavorita = true,
                    CategoriaId = context.Categorias.First().Id
                };

                context.Tendencias.Add(tendenciaFavoritos);
                context.SaveChanges();
            }

            // Paso 2: Fecha límite para considerar opiniones recientes (últimos 30 días)
            var fechaLimite = DateTime.Now.AddDays(-30); // ✅ También compatible

            // Paso 3: Seleccionamos productos con al menos 3 opiniones recientes ≥ 4 y promedio ≥ 4.5
            var mejoresProductos = context.Productos
                .Include(p => p.Opiniones)
                .ToList()
                .Where(p =>
                    p.Opiniones.Count(o => o.Puntuacion >= 4 && o.Fecha >= fechaLimite) >= 3 &&
                    p.Opiniones.Where(o => o.Fecha >= fechaLimite).Average(o => o.Puntuacion) >= 4.5
                ).ToList();

            // Paso 4: Limpiamos productos antiguos vinculados a la tendencia
            var productosExistentes = context.Productos.Where(p => p.TendenciaId == tendenciaFavoritos.Id).ToList();
            foreach (var p in productosExistentes)
            {
                p.TendenciaId = null;
            }

            // Paso 5: Asignar nuevos productos destacados
            foreach (var p in mejoresProductos)
            {
                p.TendenciaId = tendenciaFavoritos.Id;
            }

            context.SaveChanges();

            // Paso 6: Si ya no hay productos vinculados, eliminar la tendencia
            bool hayVinculados = context.Productos.Any(p => p.TendenciaId == tendenciaFavoritos.Id);

            if (!hayVinculados)
            {
                context.Tendencias.Remove(tendenciaFavoritos);
                context.SaveChanges();
            }
        }
    }
}
*/