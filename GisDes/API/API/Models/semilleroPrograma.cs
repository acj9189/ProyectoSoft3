namespace API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("semilleroPrograma")]
    public partial class semilleroPrograma
    {
        [Column(TypeName = "numeric")]
        public decimal Id { get; set; }

      //  public Programa Programa { get; set; }

       // public SemilleroInvestigacion SemilleroInvestigacion { get; set; }
    }
}
