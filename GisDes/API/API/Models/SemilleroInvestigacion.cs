namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SemilleroInvestigacion")]
    public partial class SemilleroInvestigacion
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Coordinador { get; set; }

        [Required]
        [StringLength(250)]
        public string ObjetivoGeneral { get; set; }

        [Required]
        [StringLength(1024)]
        public string ObjetivosEspecificos { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "numeric")]
        public decimal LineaInvestigacion { get; set; }

        [Required]
        [StringLength(30)]
        public string Enlace { get; set; }

        [Required]
        [StringLength(1024)]
        public string Tags { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaUpdate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Estado { get; set; }

     //   public Integrante Integrante { get; set; }

     //   public semilleroPrograma semilleroPrograma { get; set; }

     //   public Solicitud Solicitud { get; set; }
    }
}
