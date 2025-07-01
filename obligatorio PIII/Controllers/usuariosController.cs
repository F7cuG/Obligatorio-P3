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

namespace obligatorio_PIII.Controllers
{
    public class usuariosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: usuarios
        public ActionResult Index()
        {
            var usuarios = db.usuarios.Include(u => u.roles);
            return View(usuarios.ToList());
        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.RolID = new SelectList(db.roles, "ID", "Nombre");
            return View();
        }

        // POST: usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Email,Contrasenia,RolID")] usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            ViewBag.RolID = new SelectList(db.roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // POST: usuarios/Edit/5
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
            ViewBag.RolID = new SelectList(db.roles, "ID", "Nombre", usuario.RolID);
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            usuarios usuario = db.usuarios.Find(id);
            if (usuario == null) return HttpNotFound();
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuarios usuario = db.usuarios.Find(id);
            db.usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: usuarios/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string contrasenia)
        {
            var user = db.usuarios.FirstOrDefault(u => u.Email == email && u.Contrasenia == contrasenia);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email o contraseña incorrectos");
            return View();
        }

        // GET: usuarios/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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
