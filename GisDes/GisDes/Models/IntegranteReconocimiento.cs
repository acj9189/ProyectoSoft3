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
    
    public partial class IntegranteReconocimiento
    {
        public decimal IdReconocimiento { get; set; }
        public decimal IdIntegrante { get; set; }
        public System.DateTime FechaUpdate { get; set; }
        public Nullable<decimal> Estado { get; set; }
    
        public virtual Estado Estado1 { get; set; }
        public virtual Integrante Integrante { get; set; }
        public virtual Reconocimiento Reconocimiento { get; set; }
    }
}
