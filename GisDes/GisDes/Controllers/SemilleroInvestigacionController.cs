using GisDes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Autor: Carolina Quintero Valencia
//Caso de uso: Listar Semilleros: Donde se muestran los semilleros que están almacenados en la base de datos


namespace GisDes.Controllers
{
    public class SemilleroInvestigacionController : Controller
    {
        // GET: SemilleroInvestigacion
        //Método que trae en una lista con la información del Semillero
        public ActionResult ListarSemillero()
        {
            GisdesEntity db = new GisdesEntity();


            return View(db.SemilleroInvestigacion.ToList());
        }
    }
}