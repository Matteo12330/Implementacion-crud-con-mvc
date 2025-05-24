using Microsoft.AspNetCore.Mvc;
using BiteSpot.Data;
using BiteSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BiteSpot.Helpers;

namespace BiteSpot.Controllers
{
    // Este controlador maneja todo lo relacionado a las opiniones de los usuarios
    public class OpinionController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor que recibe el contexto para acceder a la base de datos
        public OpinionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Opinion/Create
        // Este método se ejecuta cuando el usuario accede al formulario para dejar una opinión
        [HttpGet]
        public IActionResult Create(int productoId)
        {
            // Verificamos si el usuario está logueado
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Account");

            // Verificamos que el producto exista
            var producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);
            if (producto == null)
                return NotFound();

            // Enviamos el nombre del producto a la vista
            ViewBag.ProductoNombre = producto.Nombre;

            // Creamos una nueva instancia de Opinión con el ID del producto
            var opinion = new Opinion
            {
                ProductoId = productoId
            };

            // Mostramos el formulario al usuario
            return View(opinion);
        }

        // POST: Opinion/Create
        // Este método se ejecuta cuando el usuario envía su opinión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Opinion opinion)
        {
            // Nuevamente, verificamos si hay sesión activa
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Account");

            // Si el modelo no es válido, volvemos a mostrar el formulario con errores
            if (!ModelState.IsValid)
            {
                ViewBag.ProductoNombre = _context.Productos
                    .Where(p => p.Id == opinion.ProductoId)
                    .Select(p => p.Nombre)
                    .FirstOrDefault();

                return View(opinion);
            }

            // Asignamos el ID del usuario y la fecha actual a la opinión
            opinion.UsuarioId = usuarioId.Value;
            opinion.Fecha = DateTime.Now;

            // Guardamos la nueva opinión en la base de datos
            _context.Opiniones.Add(opinion);
            _context.SaveChanges();

            // Aquí se llama al método que actualiza automáticamente las tendencias
            // Este paso es clave porque si se cumplen las condiciones, el producto
            // se asignará como "Favorito de los usuarios"
            TendenciaHelper.ActualizarTendenciaFavoritos(_context);

            // Enviamos un mensaje de agradecimiento y redirigimos al detalle del producto
            TempData["Mensaje"] = "¡Gracias por dejar tu opinión!";
            return RedirectToAction("Details", "Producto", new { id = opinion.ProductoId });
        }

        // POST: Opinion/Delete
        // Este método elimina una opinión por su ID (solo se permite al admin desde la vista)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Buscamos la opinión con ese ID
            var opinion = _context.Opiniones.FirstOrDefault(o => o.Id == id);
            if (opinion == null)
                return NotFound();

            // Eliminamos la opinión
            _context.Opiniones.Remove(opinion);
            _context.SaveChanges();

            // Actualizamos las tendencias automáticamente por si ya no cumple los requisitos
            TendenciaHelper.ActualizarTendenciaFavoritos(_context);

            // Volvemos a la vista del producto que tenía esa opinión
            return RedirectToAction("Details", "Producto", new { id = opinion.ProductoId });
        }
    }
}
