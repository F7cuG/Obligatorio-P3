using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obligatorio_PIII.ViewModels
{
    public class ClimaViewModel
    {
        public string Ciudad { get; set; }

        public double Temperatura { get; set; }
        
        //En porcentaje
        public int Humedad { get; set; }

        public string Descripcion { get; set; }

        public string Icono { get; set; }

        public string IconoUrl => $"https://openweathermap.org/img/wn/{Icono}@2x.png";
    }
}
