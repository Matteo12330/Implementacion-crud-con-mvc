using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Data;
using BiteSpot.Models;
using BiteSpot.Helpers;

namespace BiteSpot.Controllers
{
    // Este controlador solo puede ser accedido por usuarios logueados gracias a este filtro personalizado
    [LoginAuthorize]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Muestra la lista de productos incluyendo su categoría y tendencia (si tuviera)
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Tendencia)
                .ToListAsync();
            return View(productos);
        }

        // Muestra los detalles de un producto específico
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Tendencia)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            var opiniones = await _context.Opiniones
                .Where(o => o.ProductoId == id)
                .Include(o => o.Usuario)
                .ToListAsync();

            if (opiniones.Any())
                producto.PromedioCalificacion = Math.Round(opiniones.Average(o => o.Puntuacion), 1);

            ViewBag.Opiniones = opiniones;

            return View(producto);
        }

        // Muestra el formulario para crear un nuevo producto
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // Recibe los datos del formulario para crear el producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.TendenciaId = null;

                // Aseguramos que la fecha esté en UTC para PostgreSQL
                producto.FechaLanzamiento = DateTime.UtcNow;

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // Carga los datos de un producto para ser editado
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // Guarda los cambios hechos al producto editado
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    producto.TendenciaId = null;

                    // Convertimos a UTC antes de guardar
                    producto.FechaLanzamiento = producto.FechaLanzamiento.ToUniversalTime();

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(p => p.Id == producto.Id))
                        return NotFound();
                    else throw;
                }
            }

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // Confirma la eliminación de un producto
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // Elimina el producto definitivamente
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
