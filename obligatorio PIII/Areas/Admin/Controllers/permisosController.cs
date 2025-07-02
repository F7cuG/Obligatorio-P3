using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Areas.Admin.Controllers
{
    public class permisosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: permisos
        public ActionResult Index()
        {
            return View(db.permisos.ToList());
        }

        // GET: permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: permisos/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.roles, "ID", "Nombre"); // Ajusta según nombres reales
            return View();
        }
        // GET: permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var permiso = db.permisos.Include(p => p.roles).FirstOrDefault(p => p.ID == id);
            if (permiso == null)
                return HttpNotFound();

            var rolActual = permiso.roles.FirstOrDefault()?.ID;
            ViewBag.RolId = new SelectList(db.roles, "ID", "Nombre", rolActual);
            return View(permiso);
        }

        // POST: permisos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre")] permisos permiso, int RolId)
        {
            if (ModelState.IsValid)
            {
                var permisoExistente = db.permisos.Include(p => p.roles).FirstOrDefault(p => p.ID == permiso.ID);
                if (permisoExistente != null)
                {
                    permisoExistente.Nombre = permiso.Nombre;

                    permisoExistente.roles.Clear();
                    var nuevoRol = db.roles.Find(RolId);
                    if (nuevoRol != null)
                        permisoExistente.roles.Add(nuevoRol);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.RolId = new SelectList(db.roles, "ID", "Nombre", RolId);
            return View(permiso);
        }

        // GET: permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            permisos permisos = db.permisos.Find(id);
            db.permisos.Remove(permisos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
