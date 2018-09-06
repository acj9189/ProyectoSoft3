using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GisDes.Models;

namespace GisDes.Controllers
{
    public class ActualizarSemilleroController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ActualizarSemillero
        public ActionResult ActualizarSemillero(int id)
        {
            ///esto av en la linea 42 de actualizarSemillero Vista
            ///@Html.DropDownList("Coordinador", null, htmlAttributes: new { @class = "form-control" })
            ActualizarSemillero actualizar = new ActualizarSemillero();
            actualizar.integrantes = CargarIntegrantes();
            actualizar.LineInvetigacion = CargarLineaInvestigacion(0);
            actualizar.semillero = ObtenerSemilleroInvestigacion(id);
            return View(actualizar);

            ///esto va en la linea 83 de actualizarsemillero
            ///@Html.DropDownList("LineaInvestigacion", null, htmlAttributes: new { @class = "form-control" })
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Nombre"></param>
        /// <param name="coordinador"></param>
        /// <param name="ObjetivoGeneral"></param>
        /// <param name="ObjetivoEspecifico"></param>
        /// <param name="LineaInvestigacion"></param>
        /// <param name="Enlace"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ActualizarSemillero(int id,String Nombre, int coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            int LineaInvestigacion, String Enlace)
        {
            Boolean a=ComprobarDatos(Nombre, coordinador, ObjetivoGeneral, ObjetivoEspecifico, LineaInvestigacion, Enlace);
            if (a)
            {
               Boolean b= GuardarSemillero(id,Nombre, coordinador, ObjetivoGeneral, ObjetivoEspecifico, LineaInvestigacion, Enlace);
                //return Redirect(ListaIntegrantes);
                if (b) {
                    return RedirectToAction("ListarSemilleros");//organizar
                }
                else
                {
                    return View();//organizar
                }
            }else
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Nombre"></param>
        /// <param name="coordinador"></param>
        /// <param name="ObjetivoGeneral"></param>
        /// <param name="ObjetivoEspecifico"></param>
        /// <param name="LineaInvestigacion"></param>
        /// <param name="Enlace"></param>
        /// <returns></returns>
        public Boolean GuardarSemillero(int id,String Nombre, int coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            int LineaInvestigacion, String Enlace)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                SemilleroInvestigacion semilleroGuardar = bd.SemilleroInvestigacion.Find(id); ;
                semilleroGuardar.Nombre = Nombre;
                semilleroGuardar.Coordinador = coordinador;
                semilleroGuardar.ObjetivoGeneral = ObjetivoGeneral;
                semilleroGuardar.ObjetivosEspecificos = ObjetivoEspecifico;
                semilleroGuardar.LineaInvestigacion = LineaInvestigacion;
                semilleroGuardar.Enlace = Enlace;
                    //semilleroGuardar.
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Integrante> CargarIntegrantes()
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerIntegrantes();
        }

        public List<LineaInvestigacion> CargarLineaInvestigacion(int Codigo)
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerlineaInvestigacions();
        }

        public SemilleroInvestigacion ObtenerSemilleroInvestigacion(int Id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                //List<SemilleroInvestigacion> semillero =;
                return  bd.SemilleroInvestigacion.Find(Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="coordinador"></param>
        /// <param name="ObjetivoGeneral"></param>
        /// <param name="ObjetivoEspecifico"></param>
        /// <param name="LineaInvestigacion"></param>
        /// <param name="Enlace"></param>
        /// <returns></returns>
        public Boolean ComprobarDatos(String Nombre, int coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            int LineaInvestigacion, String Enlace)
        {
            if (Nombre.Trim().Length > 0 && coordinador > 0 && ObjetivoGeneral.Trim().Length > 0
                && ObjetivoEspecifico.Trim().Length > 0 && LineaInvestigacion > 0 && Enlace.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}