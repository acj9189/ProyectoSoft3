using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisDes.Models
{
    public class CambiarEstadoSemillero
    {
        public List<SemilleroInvestigacion> listaSemilleros()
        {
            List<SemilleroInvestigacion> lista = new List<SemilleroInvestigacion>();
            using (GisdesEntity bd = new GisdesEntity())
            {
                if (bd.SemilleroInvestigacion != null)
                {
                    lista = bd.SemilleroInvestigacion.ToList();
                }
            }
            return lista;
        }

        public List<Estado> listaEstados()
        {
            List<Estado> lista = new List<Estado>();
            using (GisdesEntity bd = new GisdesEntity())
            {
                if (bd.Estado != null)
                {
                    lista = bd.Estado.Where(x => x.Estado1 == true).ToList();
                }
            }
            return lista;
        }

        public string[] cambiarEstadoSemillero(decimal idSemillero, decimal estado)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                try
                {
                    SemilleroInvestigacion semillero = (SemilleroInvestigacion)bd.SemilleroInvestigacion.Where(x => x.Id == idSemillero);
                    semillero.Estado = estado;
                    bd.SaveChanges();
                    return new string[] { "success", "Operacion exitosa", "Se a cambiado el semillero:" + semillero.Nombre + " a estado:" + semillero.Estado1.Nombre };
                }
                catch (Exception e)
                {
                    return new string[] { "error", "error", e.ToString() };
                    throw;
                }

            }
        }
    }
}