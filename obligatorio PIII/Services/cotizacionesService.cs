using App.Models;
using App.Models.ModelsServices;
using Newtonsoft.Json;
using obligatorio_PIII.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace App.Services
{
    public class Cotizaciones
    {
        private obligatorioP3Entities1 db = new obligatorioP3Entities1();

        public async Task<List<cotizaciones>> ObtenerCotizacionesAsync()
        {

            // buscar la en la bd las cotizciones de la fecha de hoy
            var hoy = DateTime.Today;
            var cotizacionesDb = await db.cotizaciones
                .Where(m => DbFunctions.TruncateTime(m.Fecha) == hoy && (m.TipoMoneda == "USD" || m.TipoMoneda == "EUR" || m.TipoMoneda == "BRL"))
                .ToListAsync();

            if (cotizacionesDb.Count >= 3)
            {
                return cotizacionesDb;
            }

            // llamado a api
            var apiKey = ConfigurationManager.AppSettings["CurrencyLayerApiKey"];
            var symbols = "UYU,BRL,EUR";
            var url = $"http://api.currencylayer.com/live?access_key={apiKey}&currencies={symbols}&format=1";

            List<cotizaciones> nuevasCotizaciones = new List<cotizaciones>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    var data = JsonConvert.DeserializeObject<RespuestaCurrencyLayer>(response);

                    if (data.Exito)
                    {
                        var tasaUSDUYU = data.Cotizaciones["USDUYU"];
                        var tasaUSDBRL = data.Cotizaciones["USDBRL"];
                        var tasaUSDEUR = data.Cotizaciones["USDEUR"];

                        nuevasCotizaciones.Add(new cotizaciones { TipoMoneda = "USD", Valor = tasaUSDUYU, Fecha = DateTime.Now });

                        var valorEURenUYU = tasaUSDUYU / tasaUSDEUR;
                        nuevasCotizaciones.Add(new cotizaciones { TipoMoneda = "EUR", Valor = valorEURenUYU, Fecha = DateTime.Now });

                        var valorBRLenUYU = tasaUSDUYU / tasaUSDBRL;
                        nuevasCotizaciones.Add(new cotizaciones { TipoMoneda = "BRL", Valor = valorBRLenUYU, Fecha = DateTime.Now });

                        // Limpiar cotizaciones viejas y guardar las nuevas
                        db.cotizaciones.RemoveRange(cotizacionesDb);
                        db.cotizaciones.AddRange(nuevasCotizaciones);
                        await db.SaveChangesAsync();

                        return nuevasCotizaciones;
                    }
                }
            }
            catch (Exception ex)
            {
                // si falla la api, devuelvo las viejas
                return cotizacionesDb;
            }

            // si todo falla, lista vacia 
            return new List<cotizaciones>();

        }
    }
}