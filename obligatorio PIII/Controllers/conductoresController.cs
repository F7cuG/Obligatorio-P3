using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Controllers
{
    public class conductoresController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: conductores
        public ActionResult Index()
        {
            return View(db.Conductores.ToList());
        }

        // GET: conductores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            conductores conductores = db.Conductores.Find(id);
            if (conductores == null)
                return HttpNotFound();

            return View(conductores);
        }

        // GET: conductores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: conductores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Bio")] conductores conductores, HttpPostedFileBase FotoArchivo)
        {
            if (ModelState.IsValid)
            {
                if (FotoArchivo != null && FotoArchivo.ContentLength > 0)
                {
                    string extension = Path.GetExtension(FotoArchivo.FileName);
                    string nombreArchivo = Guid.NewGuid().ToString() + extension;
                    string ruta = Path.Combine(Server.MapPath("~/Uploads"), nombreArchivo);
                    FotoArchivo.SaveAs(ruta);
                    conductores.Foto = nombreArchivo;
                }

                db.Conductores.Add(conductores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conductores);
        }

        // GET: conductores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            conductores conductores = db.Conductores.Find(id);
            if (conductores == null)
                return HttpNotFound();

            return View(conductores);
        }

        // POST: conductores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Bio")] conductores datosNuevos, HttpPostedFileBase FotoArchivo)
        {
            if (ModelState.IsValid)
            {
                var conductorExistente = db.Conductores.Find(datosNuevos.ID);
                if (conductorExistente == null)
                    return HttpNotFound();

                // Actualizar los campos básicos
                conductorExistente.Nombre = datosNuevos.Nombre;
                conductorExistente.Bio = datosNuevos.Bio;

                // Manejar archivo nuevo
                if (FotoArchivo != null && FotoArchivo.ContentLength > 0)
                {
                    string extension = Path.GetExtension(FotoArchivo.FileName);
                    string nombreArchivo = Guid.NewGuid().ToString() + extension;
                    string ruta = Path.Combine(Server.MapPath("~/Uploads"), nombreArchivo);
                    FotoArchivo.SaveAs(ruta);
                    conductorExistente.Foto = nombreArchivo;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datosNuevos);
        }

        // GET: conductores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            conductores conductores = db.Conductores.Find(id);
            if (conductores == null)
                return HttpNotFound();

            return View(conductores);
        }

        // POST: conductores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conductores conductores = db.Conductores.Find(id);
            if (conductores != null)
            {
                db.Conductores.Remove(conductores);
                db.SaveChanges();
            }
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
