using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BiteSpot.Helpers;
using BiteSpot.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using BiteSpot.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiteSpot.Controllers
{
    // Este controlador maneja la navegación principal de la app, 
    // incluyendo la separación entre el administrador y los usuarios normales
    [LoginAuthorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Inyectamos el logger y el contexto de base de datos
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Esta acción es la primera que se ejecuta al iniciar sesión
        // Verifica si el usuario es el admin para redirigirlo al panel correspondiente
        public IActionResult Index()
        {
            var correo = HttpContext.Session.GetString("UsuarioCorreo");

            // Si es el admin, lo mandamos a su panel
            if (correo == "admin@bitespot.com")
            {
                return RedirectToAction("AdminPanel");
            }

            // Caso contrario, lo mandamos a la vista de usuario normal
            return RedirectToAction("VistaUsuario");
        }

        // Vista reservada exclusivamente para el administrador
        public IActionResult AdminPanel()
        {
            return View("AdminPanel");
        }

        // Vista para usuarios normales: aquí mostramos los productos
        // incluyendo los que están en tendencia
        public IActionResult VistaUsuario()
        {
            // Obtenemos productos que cumplen con los requisitos de tendencia:
            // Al menos 3 opiniones y un promedio de 4 o más
            var productosTendencia = _context.Productos
                .Include(p => p.Tendencia)
                .Include(p => p.Opiniones)
                .Where(p => p.Opiniones.Count >= 3 && p.Opiniones.Average(o => o.Puntuacion) >= 4)
                .ToList();

            // Recorremos esos productos para asegurarnos que su tendencia esté marcada como favorita
            foreach (var producto in productosTendencia)
            {
                if (producto.Tendencia != null && !producto.Tendencia.EsFavorita)
                {
                    producto.Tendencia.EsFavorita = true;
                    _context.Update(producto.Tendencia);
                }
            }

            // Guardamos cambios en la base si hicimos alguna actualización
            _context.SaveChanges();

            // Ahora cargamos todos los productos para que el usuario los vea
            var todosLosProductos = _context.Productos
                .Include(p => p.Tendencia)
                .ToList();

            // Enviamos ambos datos a la vista: los destacados y todos los productos
            ViewBag.ProductosTendencia = productosTendencia;
            return View("VistaUsuario", todosLosProductos);
        }

        // Vista opcional de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}