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
    [LoginAuthorize]
    public class TendenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TendenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tendencias = await _context.Tendencias
                .Include(t => t.Categoria)
                .ToListAsync();
            return View(tendencias);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tendencia = await _context.Tendencias
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tendencia == null) return NotFound();

            return View(tendencia);
        }

        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,CategoriaId")] Tendencia tendencia)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", tendencia.CategoriaId);
                return View(tendencia);
            }

            // Asignar fecha de creación UTC
            tendencia.FechaCreacion = DateTime.UtcNow;

            _context.Tendencias.Add(tendencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tendencia = await _context.Tendencias.FindAsync(id);
            if (tendencia == null) return NotFound();

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", tendencia.CategoriaId);
            return View(tendencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,CategoriaId")] Tendencia tendencia)
        {
            if (id != tendencia.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Preservamos la fecha de creación original
                    var original = await _context.Tendencias.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                    if (original != null)
                    {
                        tendencia.FechaCreacion = original.FechaCreacion;
                    }

                    _context.Update(tendencia);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tendencias.Any(e => e.Id == tendencia.Id)) return NotFound();
                    else throw;
                }
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", tendencia.CategoriaId);
            return View(tendencia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tendencia = await _context.Tendencias
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tendencia == null) return NotFound();

            return View(tendencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tendencia = await _context.Tendencias.FindAsync(id);
            if (tendencia != null)
                _context.Tendencias.Remove(tendencia);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public JsonResult GetPorCategoria(int id)
        {
            var tendencias = _context.Tendencias
                .Where(t => t.CategoriaId == id)
                .Select(t => new { t.Id, t.Nombre })
                .ToList();

            return Json(tendencias);
        }

        private bool TendenciaExists(int id)
        {
            return _context.Tendencias.Any(e => e.Id == id);
        }
    }
}
