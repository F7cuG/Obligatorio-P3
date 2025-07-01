using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorio_PIII.Models;
using obligatorio_PIII.ViewModels;

namespace obligatorio_PIII.Controllers
{
    public class rolesController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: roles
        public ActionResult Index()
        {
            var rolesConPermisos = db.roles.Include(r => r.permisos);
            return View(rolesConPermisos.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rol = db.roles.Include("permisos").FirstOrDefault(r => r.ID == id);

            if (rol == null)
                return HttpNotFound();

            return View(rol);
        }


        // GET: roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre")] roles roles)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rol = db.roles.Include("permisos").FirstOrDefault(r => r.ID == id);
            if (rol == null)
                return HttpNotFound();

            var viewModel = new rolPermisosView
            {
                ID = rol.ID,
                Nombre = rol.Nombre,
                TodosLosPermisos = db.permisos.ToList(),
                PermisosSeleccionados = rol.permisos.Select(p => p.ID).ToList()
            };

            return View(viewModel);
        }

        // POST: roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(rolPermisosView model)
        {
            if (ModelState.IsValid)
            {
                var rol = db.roles.Include("permisos").FirstOrDefault(r => r.ID == model.ID);
                if (rol == null)
                    return HttpNotFound();

                rol.Nombre = model.Nombre;

                // Actualizar permisos
                rol.permisos.Clear();
                if (model.PermisosSeleccionados != null)
                {
                    var permisosSeleccionados = db.permisos
                        .Where(p => model.PermisosSeleccionados.Contains(p.ID)).ToList();
                    foreach (var permiso in permisosSeleccionados)
                        rol.permisos.Add(permiso);
                }

                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            model.TodosLosPermisos = db.permisos.ToList();
            return View(model);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            roles roles = db.roles.Find(id);
            if (roles == null)
                return HttpNotFound();

            return View(roles);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            roles roles = db.roles.Find(id);
            db.roles.Remove(roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
