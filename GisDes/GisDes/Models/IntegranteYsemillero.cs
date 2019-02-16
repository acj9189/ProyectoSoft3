using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisDes.Models
{
    public class IntegranteYsemillero
    {
        public List<Integrante> integrantes { set; get; }
        public List<SemilleroInvestigacion> semilleroInvestigacion { set; get; }
        public List<String> FechaIngreso { set; get; }
    }
}