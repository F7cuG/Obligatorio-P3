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

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string contrasenia)
        {
            using (var contexto = new obligatorioP3Entities1())  
            {
                var usuario = contexto.usuarios
                    .FirstOrDefault(u => u.Email == email && u.Contrasenia == contrasenia);

                if (usuario != null)
                {
                    // Guardar datos del usuario en sesión
                    Session["UsuarioID"] = usuario.ID;
                    Session["UsuarioNombre"] = usuario.Nombre;
                    Session["UsuarioRolID"] = usuario.RolID;

                    return RedirectToAction("Index", "Home");
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
 