using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obligatorio_PIII.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
