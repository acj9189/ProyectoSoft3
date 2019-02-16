using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisDes.Models
{
    public class ConsultaSemilleroInfo
    {

        //public List<Integrante> listaIntegrantesDocentes { get; set; }
        //public List<LineaInvestigacion> listaLineasInvestigacion { get; set; }

        public SemilleroInvestigacion SemilleroInvestigacionNombre { get; set; }
        public List<SemilleroInvestigacion> SemilleroInvestigacionTag { get; set; }
        public List<Integrante> listaIntegrantes { get; set; }
    }
}