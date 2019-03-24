namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programa")]
    public partial class Programa
    {
        [Column(TypeName = "numeric")]
        public decimal Id { get; set; }

        [Required]
        public string nombre { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

       // public semilleroPrograma semilleroPrograma { get; set; }
    }
}
