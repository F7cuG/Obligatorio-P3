using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login (SIN validación de AntiForgeryToken)
        [HttpPost]
        public ActionResult Index(string email, string contrasenia)
        {
            using (var contexto = new obligatorioP3Entities1())
            {
                var usuario = contexto.Usuarios
                    .Include(u => u.roles) // Incluye el nombre del rol
                    .FirstOrDefault(u => u.Email == email && u.Contrasenia == contrasenia);

                if (usuario != null)
                {
                    // Guardar datos del usuario en sesión
                    Session["UsuarioID"] = usuario.ID;
                    Session["UsuarioNombre"] = usuario.Nombre;
                    Session["UsuarioRolID"] = usuario.RolID;
                    Session["UsuarioRolNombre"] = usuario.roles.Nombre;

                    // Redirigir según el rol
                    if (usuario.roles.Nombre == "Administrador")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email o contraseña incorrectos.");
                    return View();
                }
            }
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
