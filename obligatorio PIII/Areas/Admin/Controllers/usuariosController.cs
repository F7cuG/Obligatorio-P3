using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Areas.Admin.Controllers
{
    public class usuariosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // 🚀 Esto hace que todas las vistas usen _LayoutAdmin por defecto
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // Aplica sólo si es una vista
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = "~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml";
            }
        }

        // GET: usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.roles);
            return View(usuarios.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            return View(usuario);
        }

        public ActionResult Create()
        {
            ViewBag.RolID = new SelectList(db.Usuarios, "ID", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Email,Contrasenia,RolID")] usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            ViewBag.RolID = new SelectList(db.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Email,Contrasenia,RolID")] usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolID = new SelectList(db.Roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuarios usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string contrasenia)
        {
            var user = db.Usuarios
                .Include(u => u.roles)
                .FirstOrDefault(u => u.Email == email && u.Contrasenia == contrasenia);

            if (user != null)
            {
                // Asegurarse que el rol venga en UserData
                string rolString = user.roles.Nombre;

                var authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Email,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    rolString
                );

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(cookie);

                //Sesiones
                Session["UsuarioID"] = user.ID;
                Session["UsuarioNombre"] = user.Nombre;
                Session["UsuarioRolID"] = user.RolID;
                Session["UsuarioRolNombre"] = user.roles.Nombre;

                // Redirección según rol
                if (user.roles.Nombre == "Administrador")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError("", "Email o contraseña incorrectos");
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "usuarios", new { area = "Admin" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
