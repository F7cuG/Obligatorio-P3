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
    public class programasController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: programas
        public ActionResult Index()
        {
            var programas = db.programas.Include(p => p.conductores);
            return View(programas.ToList());
        }

        // GET: programas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programas programas = db.programas.Find(id);
            if (programas == null)
            {
                return HttpNotFound();
            }
            return View(programas);
        }

        // GET: programas/Create
        public ActionResult Create()
        {
            ViewBag.ConductoresID = new SelectList(db.conductores, "ID", "Nombre");
            return View();
        }

        // POST: programas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Imagen,Descripcion,Horario,ConductoresID")] programas programas)
        {
            if (ModelState.IsValid)
            {
                db.programas.Add(programas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConductoresID = new SelectList(db.conductores, "ID", "Nombre", programas.ConductoresID);
            return View(programas);
        }

        // GET: programas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programas programas = db.programas.Find(id);
            if (programas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConductoresID = new SelectList(db.conductores, "ID", "Nombre", programas.ConductoresID);
            return View(programas);
        }

        // POST: programas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Imagen,Descripcion,Horario,ConductoresID")] programas programas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConductoresID = new SelectList(db.conductores, "ID", "Nombre", programas.ConductoresID);
            return View(programas);
        }

        // GET: programas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            programas programas = db.programas.Find(id);
            if (programas == null)
            {
                return HttpNotFound();
            }
            return View(programas);
        }

        // POST: programas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            programas programas = db.programas.Find(id);
            db.programas.Remove(programas);
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
