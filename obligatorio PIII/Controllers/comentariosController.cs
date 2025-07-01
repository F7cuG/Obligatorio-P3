using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Controllers
{
    public class comentariosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: comentarios
        public ActionResult Index()
        {
            var comentarios = db.comentarios.Include(c => c.clientes);
            return View(comentarios.ToList());
        }

        // GET: comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentarios comentarios = db.comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // GET: comentarios/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.clientes, "ID", "Nombre");
            return View();
        }

        // POST: comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClienteID,ProgramaID,Comentario,Fecha")] comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.comentarios.Add(comentarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.clientes, "ID", "Nombre", comentarios.ClienteID);
            return View(comentarios);
        }

        // GET: comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentarios comentarios = db.comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.clientes, "ID", "Nombre", comentarios.ClienteID);
            return View(comentarios);
        }

        // POST: comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteID,ProgramaID,Comentario,Fecha")] comentarios comentarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.clientes, "ID", "Nombre", comentarios.ClienteID);
            return View(comentarios);
        }

        // GET: comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comentarios comentarios = db.comentarios.Find(id);
            if (comentarios == null)
            {
                return HttpNotFound();
            }
            return View(comentarios);
        }

        // POST: comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comentarios comentarios = db.comentarios.Find(id);
            db.comentarios.Remove(comentarios);
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
