using Microsoft.AspNetCore.Mvc;
using BiteSpot.Data;
using BiteSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Services;
using BiteSpot.Factories;

namespace BiteSpot.Controllers
{
    public class OpinionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITendenciaService _tendenciaService;

        public OpinionController(ApplicationDbContext context, ITendenciaService tendenciaService)
        {
            _context = context;
            _tendenciaService = tendenciaService;
        }

        [HttpGet]
        public IActionResult Create(int productoId)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Account");

            var producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);
            if (producto == null)
                return NotFound();

            ViewBag.ProductoNombre = producto.Nombre;

            var opinion = new Opinion
            {
                ProductoId = productoId
            };

            return View(opinion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Opinion opinion)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.ProductoNombre = _context.Productos
                    .Where(p => p.Id == opinion.ProductoId)
                    .Select(p => p.Nombre)
                    .FirstOrDefault();

                return View(opinion);
            }

            var nuevaOpinion = OpinionFactory.CrearOpinion(
                usuarioId.Value,
                opinion.ProductoId,
                opinion.Comentario,
                opinion.Puntuacion
            );

            _context.Opiniones.Add(nuevaOpinion);
            _context.SaveChanges();

            _tendenciaService.ActualizarTendenciaFavoritos();

            TempData["Mensaje"] = "¡Gracias por dejar tu opinión!";
            return RedirectToAction("Details", "Producto", new { id = nuevaOpinion.ProductoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var opinion = _context.Opiniones.FirstOrDefault(o => o.Id == id);
            if (opinion == null)
                return NotFound();

            _context.Opiniones.Remove(opinion);
            _context.SaveChanges();

            _tendenciaService.ActualizarTendenciaFavoritos();

            return RedirectToAction("Details", "Producto", new { id = opinion.ProductoId });
        }
    }
}
