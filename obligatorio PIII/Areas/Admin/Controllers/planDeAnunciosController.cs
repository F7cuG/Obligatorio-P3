using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Areas.Admin.Controllers 

{
    public class planDeAnunciosController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: planDeAnuncios
        public ActionResult Index()
        {
            return View(db.PlanDeAnuncios.ToList());
        }

        // GET: planDeAnuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            PlanDeAnuncios plan = db.PlanDeAnuncios.Find(id);
            if (plan == null) return HttpNotFound();
            return View(plan);
        }

        // GET: planDeAnuncios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: planDeAnuncios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Duracion,Frecuencia,Estado")] PlanDeAnuncios plan)
        {
            if (ModelState.IsValid)
            {
                db.PlanDeAnuncios.Add(plan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        // GET: planDeAnuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            PlanDeAnuncios plan = db.PlanDeAnuncios.Find(id);
            if (plan == null) return HttpNotFound();
            return View(plan);
        }

        // POST: planDeAnuncios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Duracion,Frecuencia,Estado")] PlanDeAnuncios plan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        // GET: planDeAnuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            PlanDeAnuncios plan = db.PlanDeAnuncios.Find(id);
            if (plan == null) return HttpNotFound();
            return View(plan);
        }

        // POST: planDeAnuncios/Delete/5
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
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
