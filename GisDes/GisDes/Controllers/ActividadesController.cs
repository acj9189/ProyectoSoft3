using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GisDes.Models;
using System.Diagnostics;
namespace GisDes.Controllers
{
    public class ActividadesController : Controller
    {
        private ActividadesView  ActividadesView = new ActividadesView();
        public ActionResult ActividadesPorSemillero()
        {

            using (GisdesEntity bd = new GisdesEntity())
            {
                {
                    if (ActividadesView.semilleroInvestigacion == null)
                    {
                        ActividadesView.semilleroInvestigacion = bd.SemilleroInvestigacion.ToList();
                        ActividadesView.investigacions = new List<LineaInvestigacion>();
                        ActividadesView.investigacions.Add(null);
                    }
                    return View(ActividadesView);
                }

            }



            public ActionResult BuscarIDSemillero(String nombre)
            {
                Debug.WriteLine("--------------------------" + nombre);
                Debug.WriteLine("--------------------------" + nombre);
                Debug.WriteLine("--------------------------" + nombre);
                Debug.WriteLine("--------------------------" + nombre);


                using (GisdesEntity bd = new GisdesEntity())
                {
                    List<SemilleroInvestigacion> Semillero = bd.SemilleroInvestigacion.Where(SemilleroInvestigacion => SemilleroInvestigacion.Nombre == nombre).ToList();


                    if (Semillero.LongCount() > 0)
                    {
                        return View(ListarActividades(Semillero.First().Nombre));
                    }

                    return null;
                }
            }


            private ActividadesView ListarActividades(String Nombre)
            {
                using (GisdesEntity bd = new GisdesEntity())
                {
                    List<SemilleroInvestigacion> Semillero = bd.SemilleroInvestigacion.Where(SemilleroInvestigacion => SemilleroInvestigacion.Nombre == Nombre).ToList();

                    decimal idSemillero = Semillero.First().Id;
                    List<IntegranteSemilleroInvestigacion> idsintegrantes = bd.IntegranteSemilleroInvestigacion.Where(IntegranteSemilleroInvestigacion => IntegranteSemilleroInvestigacion.IdSemillero == idSemillero).ToList();
                    List<Integrante> integrantes = new List<Integrante>();
                    foreach (var item in idsintegrantes)
                    {
                        List<Integrante> inte = bd.Integrante.Where(integ => integ.Id == item.IdIntegrante).ToList();
                        integrantes.Add(inte.First());
                    }
                    ActividadesView.integrantes = integrantes;

                    return ActividadesView;
                }
            }
        }
    }
}