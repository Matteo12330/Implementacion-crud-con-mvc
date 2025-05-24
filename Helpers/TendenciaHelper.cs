using BiteSpot.Data;
using BiteSpot.Models;
using Microsoft.EntityFrameworkCore;

namespace BiteSpot.Helpers
{
    // Esta clase se encarga de actualizar la tendencia especial llamada "Favoritos de los usuarios"
    public static class TendenciaHelper
    {
        // Se llama a este metodo cada vez que se crea una opinión nueva
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
                    FechaCreacion = DateTime.Now,
                    EsFavorita = true,
                    // Se le asigna alguna categoría válida para que no falle la relación
                    CategoriaId = context.Categorias.First().Id
                };

                // Guardamos la nueva tendencia en la base de datos
                context.Tendencias.Add(tendenciaFavoritos);
                context.SaveChanges();
            }

            // Paso 2: Fecha límite para considerar opiniones recientes (últimos 30 días)
            var fechaLimite = DateTime.Now.AddDays(-30);

            // Paso 3: Seleccionamos productos que cumplan con estas condiciones:
            // - Tengan al menos 3 opiniones con puntuación mayor o igual a 4
            // - Esas opiniones deben haber sido hechas dentro de los últimos 30 días
            // - El promedio de esas opiniones recientes debe ser mayor o igual a 4.5
            var mejoresProductos = context.Productos
                .Include(p => p.Opiniones)
                .ToList()
                .Where(p =>
                    p.Opiniones.Count(o => o.Puntuacion >= 4 && o.Fecha >= fechaLimite) >= 3 &&
                    p.Opiniones.Where(o => o.Fecha >= fechaLimite).Average(o => o.Puntuacion) >= 4.5
                ).ToList();

            // Paso 4: Limpiamos todos los productos que estaban en la tendencia antes
            var productosExistentes = context.Productos.Where(p => p.TendenciaId == tendenciaFavoritos.Id).ToList();
            foreach (var p in productosExistentes)
            {
                // Les quitamos la tendencia (esto evita que se acumulen productos viejos)
                p.TendenciaId = null;
            }

            // Paso 5: Asignamos los nuevos productos favoritos a la tendencia
            foreach (var p in mejoresProductos)
            {
                p.TendenciaId = tendenciaFavoritos.Id;
            }

            // Finalmente guardamos los cambios en la base de datos
            context.SaveChanges();
        }
    }
}