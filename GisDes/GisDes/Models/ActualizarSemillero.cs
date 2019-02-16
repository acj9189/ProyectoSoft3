using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisDes.Models
{
    public class ActualizarSemillero
    {
        public List<Integrante> integrantes { set; get; }
        public List<LineaInvestigacion> LineInvetigacion { set; get; }
        public SemilleroInvestigacion semillero { set; get; }


        public List<Integrante> ObtenerIntegrantes()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                this.integrantes = bd.Integrante.ToList();

            }
              
            return this.integrantes;
        }

        public List<LineaInvestigacion> ObtenerlineaInvestigacions()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                this.LineInvetigacion = bd.LineaInvestigacion.ToList();

            }

            return this.LineInvetigacion;
        }
    }
}