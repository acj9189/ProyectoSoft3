using GisDes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GisDes.Controllers
{
    public class EstadoController : Controller
    {
        /// <summary>
        /// Metodo: el metodo index se encarga de enviar la informacion a la visa con los posibles estados a utilizar en el proyecto.
        /// Caso de uso: CRUD Estado
        /// Creado: Jesus Daniel Gomez G.
        /// Version: 1.0
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                List<Estado> listaEstadosActivos = bd.Estado.Where(estado => estado.Estado1 == true).ToList();
                return View(listaEstadosActivos);
            }
        }
        
    }
}