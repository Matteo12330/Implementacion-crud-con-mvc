using Microsoft.AspNetCore.Mvc;
using BiteSpot.Data;
using BiteSpot.Models;
using BiteSpot.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BiteSpot.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // REGISTRO
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
                {
                    ModelState.AddModelError("Correo", "Este correo ya está en uso.");
                    return View(usuario);
                }

                usuario.Contrasena = SeguridadHelper.CifrarMD5(usuario.Contrasena);
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                TempData["RegistroExitoso"] = "¡Registro exitoso! Ahora puedes iniciar sesión.";
                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // LOGIN
        public IActionResult Login()
        {
            ViewBag.Mensaje = TempData["RegistroExitoso"];
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string passwordCifrada = SeguridadHelper.CifrarMD5(model.Contrasena);

            var usuario = _context.Usuarios.FirstOrDefault(u =>
                u.Correo == model.Correo && u.Contrasena == passwordCifrada);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(model);
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
