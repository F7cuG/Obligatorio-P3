using System.Linq;
using System.Web.Mvc;
using obligatorio_PIII.Models;

namespace obligatorio_PIII.Controllers
{
    public class PublicPatrocinadoresController : Controller
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        // GET: PublicPatrocinadores
        public ActionResult Index()
        {
            var patrocinadores = db.patrocinadores.ToList();
            return View(patrocinadores);
        }
    }
}
