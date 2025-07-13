using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Controllers
{
    public class patrocinadoresController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: patrocinadores
        public ActionResult Index()
        {
            var patrocinadores = db.patrocinadores.Include(p => p.PlanDeAnuncios);
            return View(patrocinadores.ToList());
        }

        // GET: patrocinadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            patrocinadores patrocinador = db.patrocinadores
                .Include(p => p.PlanDeAnuncios)
                .FirstOrDefault(p => p.ID == id);

            if (patrocinador == null)
                return HttpNotFound();

            return View(patrocinador);
        }

        // GET: patrocinadores/Create
        public ActionResult Create()
        {
            ViewBag.PlanDeAnunciosID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre");
            return View();
        }

        // POST: patrocinadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Descripcion,PlanDeAnunciosID")] patrocinadores patrocinadores)
        {
            if (ModelState.IsValid)
            {
                db.patrocinadores.Add(patrocinadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanDeAnunciosID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinadores.PlanDeAnunciosID);
            return View(patrocinadores);
        }

        // GET: patrocinadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            patrocinadores patrocinadores = db.patrocinadores.Find(id);
            if (patrocinadores == null)
                return HttpNotFound();

            ViewBag.PlanDeAnunciosID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinadores.PlanDeAnunciosID);
            return View(patrocinadores);
        }

        // POST: patrocinadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Descripcion,PlanDeAnunciosID")] patrocinadores patrocinadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patrocinadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanDeAnunciosID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinadores.PlanDeAnunciosID);
            return View(patrocinadores);
        }

        // GET: patrocinadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            patrocinadores patrocinadores = db.patrocinadores
                .Include(p => p.PlanDeAnuncios)
                .FirstOrDefault(p => p.ID == id);

            if (patrocinadores == null)
                return HttpNotFound();

            return View(patrocinadores);
        }

        // POST: patrocinadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            patrocinadores patrocinadores = db.patrocinadores.Find(id);
            db.patrocinadores.Remove(patrocinadores);
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
