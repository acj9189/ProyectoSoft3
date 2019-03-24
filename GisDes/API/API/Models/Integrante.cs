namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Integrante")]
    public partial class Integrante
    {
     
 
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(30)]
        public string Documento { get; set; }

        [Required]
        [StringLength(30)]
        public string Nacionalidad { get; set; }

        [Required]
        [StringLength(50)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(30)]
        public string Cvlac { get; set; }

        [Column(TypeName = "numeric")]
        public decimal NivelAcademico { get; set; }

        [Required]
        [StringLength(30)]
        public string Correo { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaIngreso { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TipoIntegrante { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaUpdate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Estado { get; set; }

  

       // public Solicitud Solicitud { get; set; }

       // public Solicitud Solicitud1 { get; set; }
    }
}
