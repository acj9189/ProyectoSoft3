//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GisDes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoIntegrante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoIntegrante()
        {
            this.Integrante = new HashSet<Integrante>();
        }
    
        public decimal Id { get; set; }
        public string Nombre { get; set; }
        public System.DateTime FechaUpdate { get; set; }
        public Nullable<decimal> Estado { get; set; }
    
        public virtual Estado Estado1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Integrante> Integrante { get; set; }
    }
}
