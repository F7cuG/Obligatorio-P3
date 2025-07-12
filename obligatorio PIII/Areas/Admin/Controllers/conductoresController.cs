using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class conductoresController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: Admin/conductores
        public ActionResult Index()
        {
            var conductores = db.conductores.ToList();
            return View(conductores);
        }

        // GET: Admin/conductores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/conductores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(conductores conductor)
        {
            if (ModelState.IsValid)
            {
                db.conductores.Add(conductor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conductor);
        }

        // GET: Admin/conductores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var conductor = db.conductores.Find(id);
            if (conductor == null)
                return HttpNotFound();

            return View(conductor);
        }

        // POST: Admin/conductores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(conductores conductor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conductor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conductor);
        }

        // GET: Admin/conductores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var conductor = db.conductores.Find(id);
            if (conductor == null)
                return HttpNotFound();

            return View(conductor);
        }

        // POST: Admin/conductores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var conductor = db.conductores.Find(id);
            db.conductores.Remove(conductor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
