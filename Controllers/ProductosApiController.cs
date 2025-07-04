using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Data;
using BiteSpot.Models;
using System.Linq;

namespace BiteSpot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductosApi/favoritos
        [HttpGet("favoritos")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductosFavoritos()
        {
            var productos = await _context.Productos
                .Include(p => p.Opiniones)
                .ToListAsync();

            var favoritos = productos
                .Where(p => p.Opiniones.Count(o => o.Puntuacion >= 4) >= 3)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.ImagenUrl,
                    PromedioCalificacion = p.Opiniones.Any()
                        ? Math.Round(p.Opiniones.Average(o => o.Puntuacion), 1)
                        : 0,
                    Tendencia = "Favoritos de los usuarios"
                })
                .ToList();

            return Ok(favoritos);
        }
    }
}