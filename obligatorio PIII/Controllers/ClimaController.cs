using System.Threading.Tasks;
using System.Web.Mvc;
using obligatorio_PIII.Services;
using obligatorio_PIII.ViewModels;

namespace obligatorio_PIII.Controllers
{
    public class ClimaController : Controller
    {
        private readonly ClimaService _climaService = new ClimaService();

        // GET: /Clima/
        public ActionResult Index()
        {
            // Ciudad por defecto
            return View(new ClimaViewModel { Ciudad = "Montevideo,uy" });
        }

        // POST: /Clima/
        [HttpPost]
        public async Task<ActionResult> Index(string ciudad)
        {
            if (string.IsNullOrWhiteSpace(ciudad))

                return View(new ClimaViewModel());

            var data = await _climaService.GetWeatherByCityAsync(ciudad);


            var vm = new ClimaViewModel
            {
                Ciudad = ciudad,
                Temperatura = data.Main.Temp ?? 0,
                Humedad = (int) (data.Main.Humidity ?? 0),
                Descripcion = data.Weather[0].Description,
                Icono = data.Weather[0].Icon
            };

            return View(vm);
        }
    }
}
