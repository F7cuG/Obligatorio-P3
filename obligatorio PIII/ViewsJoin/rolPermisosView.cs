using System.Collections.Generic;
using obligatorio_PIII.Models;
using System.ComponentModel.DataAnnotations;

namespace obligatorio_PIII.ViewModels
{
    public class rolPermisosView
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre del Rol")]
        public string Nombre { get; set; }

        [Display(Name = "Permisos asignados")]
        public List<int> PermisosSeleccionados { get; set; }

        public List<permisos> TodosLosPermisos { get; set; }
    }
}
