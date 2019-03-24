namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistroApp")]
    public partial class RegistroApp
    {
        [Column(TypeName = "numeric")]
        public decimal Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        public string Carrera { get; set; }

        [Required]
        public string Codigo { get; set; }
    }
}
