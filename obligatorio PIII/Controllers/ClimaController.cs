using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obligatorio_PIII.Controllers
{
    public class ClimaController : Controller
    {
        // GET: Clima
        public ActionResult Index()
        {


 
            var client = new RestClient("api.openweathermap.org/data/2.5/forecast?lat=-34.9&lon=54.95&appid=b38bb7a3481915276b6d91a37339d6bd");
            var request = new RestRequest("", Method.Get);
            RestResponse response = client.Execute(request);
            var json = response.Content;

            return View();
        }
    }
}