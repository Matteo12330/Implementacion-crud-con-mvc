using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Data;
using BiteSpot.Models;

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
            var favoritos = await _context.Productos
                .Include(p => p.Tendencia)
                .Where(p => p.Tendencia != null && p.Tendencia.EsFavorita == true)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.ImagenUrl,
                    PromedioCalificacion = p.PromedioCalificacion,
                    Tendencia = p.Tendencia.Nombre
                })
                .ToListAsync();

            return Ok(favoritos);
        }
    }
}