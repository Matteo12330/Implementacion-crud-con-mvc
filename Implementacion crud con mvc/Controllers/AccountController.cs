using Microsoft.AspNetCore.Mvc;
using Implementacion_crud_con_mvc.Data;
using Implementacion_crud_con_mvc.Models;
using Implementacion_crud_con_mvc.Helpers;
using System.Linq;

namespace Implementacion_crud_con_mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Contrasena = SeguridadHelper.CifrarMD5(usuario.Contrasena);
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombre, string contrasena)
        {
            string passwordCifrada = SeguridadHelper.CifrarMD5(contrasena);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == nombre && u.Contrasena == passwordCifrada);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
