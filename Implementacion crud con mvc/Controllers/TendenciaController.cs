using System;
using System.Collections.Generic;
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
    public class TendenciaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TendenciaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tendencias.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendencia = await _context.Tendencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tendencia == null)
            {
                return NotFound();
            }

            return View(tendencia);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaCreacion")] Tendencia tendencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tendencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tendencia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendencia = await _context.Tendencias.FindAsync(id);
            if (tendencia == null)
            {
                return NotFound();
            }
            return View(tendencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaCreacion")] Tendencia tendencia)
        {
            if (id != tendencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tendencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TendenciaExists(tendencia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tendencia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendencia = await _context.Tendencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tendencia == null)
            {
                return NotFound();
            }

            return View(tendencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tendencia = await _context.Tendencias.FindAsync(id);
            if (tendencia != null)
            {
                _context.Tendencias.Remove(tendencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TendenciaExists(int id)
        {
            return _context.Tendencias.Any(e => e.Id == id);
        }
    }
}
