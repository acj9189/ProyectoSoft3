namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Solicitud")]
    public partial class Solicitud
    {
        [Column(TypeName = "numeric")]
        public decimal Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreIntegrante { get; set; }

        [Required]
        [StringLength(50)]
        public string Correo { get; set; }

        [Required]
        public string DescripcionPorqueQuiereIngresar { get; set; }

        //public Integrante Integrante { get; set; }

        //public Integrante Integrante1 { get; set; }

        //public SemilleroInvestigacion SemilleroInvestigacion { get; set; }
    }
}
