using obligatorio_PIII.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AplicacionWeb.Models;
using AplicacionWeb.Filters;
using obligatorio_PIII.Models;

namespace AplicacionWeb.Areas.Admin.Controllers
{
    public class PatrocinadoresController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: Admin/Patrocinadores
        public ActionResult Index()
        {
            var patrocinadores = db.patrocinadores.Include(p => p.PlanDeAnuncios).ToList();
            return View(patrocinadores);
        }

        // GET: Admin/Patrocinadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patrocinador = db.patrocinadores.Find(id);
            if (patrocinador == null) return HttpNotFound();

            return View(patrocinador);
        }

        // GET: Admin/Patrocinadores/Create
        public ActionResult Create()
        {
            ViewBag.PlanID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre");
            return View();
        }

        // POST: Admin/Patrocinadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(patrocinadores patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.patrocinadores.Add(patrocinador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinador.PlanDeAnuncios);
            return View(patrocinador);
        }

        // GET: Admin/Patrocinadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patrocinador = db.patrocinadores.Find(id);
            if (patrocinador == null) return HttpNotFound();

            ViewBag.PlanID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinador.PlanDeAnuncios);
            return View(patrocinador);
        }

        // POST: Admin/Patrocinadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(patrocinadores patrocinador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patrocinador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlanID = new SelectList(db.PlanDeAnuncios, "ID", "Nombre", patrocinador.PlanDeAnuncios);
            return View(patrocinador);
        }

        // GET: Admin/Patrocinadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var patrocinador = db.patrocinadores.Find(id);
            if (patrocinador == null) return HttpNotFound();

            return View(patrocinador);
        }

        // POST: Admin/Patrocinadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var patrocinador = db.patrocinadores.Find(id);
            db.patrocinadores.Remove(patrocinador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
