using Microsoft.Ajax.Utilities;
using obligatorio_PIII.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace obligatorio_PIII.Controllers
{
    public class HomeController : Controller
    {

        


        [Authorize]
        public ActionResult Index()
        {

            HomeViewModel modelCotizacion = new HomeViewModel();

            cotizaciones cotizcio = new cotizaciones();

            


            return View(modelCotizacion);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
