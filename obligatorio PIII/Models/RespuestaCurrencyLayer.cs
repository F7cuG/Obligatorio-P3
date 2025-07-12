using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ModelsServices
{
    public class RespuestaCurrencyLayer
    {
        [JsonProperty("success")]
        public bool Exito { get; set; }

        [JsonProperty("source")]
        public string Fuente { get; set; }

        [JsonProperty("quotes")]
        public Dictionary<string, decimal> Cotizaciones { get; set; }
    }
}