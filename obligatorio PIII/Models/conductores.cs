//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace obligatorio_PIII.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class conductores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conductores()
        {
            this.programas = new HashSet<programas>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Bio { get; set; }
        public string Foto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<programas> programas { get; set; }
    }
}
