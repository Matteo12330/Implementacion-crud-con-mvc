using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Implementacion_crud_con_mvc.Data;
using Implementacion_crud_con_mvc.Models;
using Implementacion_crud_con_mvc.Helpers;

namespace Implementacion_crud_con_mvc.Controllers
{
    [LoginAuthorize]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Tendencia)
                .ThenInclude(t => t.Categoria)
                .ToListAsync();
            return View(productos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Tendencia)
                .ThenInclude(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewBag.Tendencias = new SelectList(_context.Tendencias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            int categoriaId = _context.Tendencias
                .Where(t => t.Id == producto.TendenciaId)
                .Select(t => t.CategoriaId)
                .FirstOrDefault();

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", categoriaId);
            ViewBag.Tendencias = new SelectList(_context.Tendencias.Where(t => t.CategoriaId == categoriaId), "Id", "Nombre", producto.TendenciaId);

            return View(producto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            int categoriaId = _context.Tendencias
                .Where(t => t.Id == producto.TendenciaId)
                .Select(t => t.CategoriaId)
                .FirstOrDefault();

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", categoriaId);
            ViewBag.Tendencias = new SelectList(_context.Tendencias.Where(t => t.CategoriaId == categoriaId), "Id", "Nombre", producto.TendenciaId);

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(e => e.Id == producto.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            int categoriaId = _context.Tendencias
                .Where(t => t.Id == producto.TendenciaId)
                .Select(t => t.CategoriaId)
                .FirstOrDefault();

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", categoriaId);
            ViewBag.Tendencias = new SelectList(_context.Tendencias.Where(t => t.CategoriaId == categoriaId), "Id", "Nombre", producto.TendenciaId);

            return View(producto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Tendencia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
                _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
