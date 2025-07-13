using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Areas.Admin.Controllers
{
    public class PlanDeAnunciosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: PlanDeAnuncios
        public ActionResult Index()
        {
            var planes = db.PlanDeAnuncios.Include(p => p.patrocinadores);
            return View(planes.ToList());
        }

        // GET: PlanDeAnuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PlanDeAnuncios plan = db.PlanDeAnuncios.Include(p => p.patrocinadores).FirstOrDefault(p => p.ID == id);
            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }

        // GET: PlanDeAnuncios/Create
        public ActionResult Create()
        {
            ViewBag.PatrocinadorID = new SelectList(db.patrocinadores, "ID", "Nombre");
            return View();
        }

        // POST: PlanDeAnuncios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanDeAnuncios plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    plan.FechaCreacion = DateTime.Now;
                    plan.FechaModificacion = DateTime.Now;

                    db.PlanDeAnuncios.Add(plan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            ViewBag.PatrocinadorID = new SelectList(db.patrocinadores, "ID", "Nombre", plan.PatrocinadorID);
            return View(plan);
        }

        // GET: PlanDeAnuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PlanDeAnuncios plan = db.PlanDeAnuncios.Find(id);
            if (plan == null)
                return HttpNotFound();

            ViewBag.PatrocinadorID = new SelectList(db.patrocinadores, "ID", "Nombre", plan.PatrocinadorID);
            return View(plan);
        }

        // POST: PlanDeAnuncios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanDeAnuncios plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    plan.FechaModificacion = DateTime.Now;
                    db.Entry(plan).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            ViewBag.PatrocinadorID = new SelectList(db.patrocinadores, "ID", "Nombre", plan.PatrocinadorID);
            return View(plan);
        }

        // GET: PlanDeAnuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PlanDeAnuncios plan = db.PlanDeAnuncios.Include(p => p.patrocinadores).FirstOrDefault(p => p.ID == id);
            if (plan == null)
                return HttpNotFound();

            return View(plan);
        }

        // POST: PlanDeAnuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanDeAnuncios plan = db.PlanDeAnuncios.Find(id);
            db.PlanDeAnuncios.Remove(plan);
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
