using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GisDes.Models;


namespace GisDes.Controllers
{
    public class IntegranteController : Controller
    {
        public Integrante integrante { get; set; }
        // GET: Integrante
        public ActionResult CrearIntegrante()
        {
            Console.WriteLine("");
            return View();
        }

        //POST: Integrante
        /// <summary>
        /// Autor: Carolina Quintero Valencia
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Apellidos"></param>
        /// <param name="Cedula"></param>
        /// <param name="Nacionalidad"></param>
        /// <param name="Sexo"></param>
        /// <param name="Cvlac"></param>
        /// <param name="idNivelAcademico"></param>
        /// <param name="Correo"></param>
        /// <param name="idTipoIntegrante"></param>
        /// <param name="FechaIngreso"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearIntegrante(string Nombre, string Apellidos, string Cedula, string Nacionalidad, string Sexo, string Cvlac, string idNivelAcademico, string Correo, string idTipoIntegrante, string FechaIngreso)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                Estado estado = bd.Estado.ToList().Find(x => x.Nombre.Equals("Activo"));
                Integrante integrante = bd.Integrante.ToList().Find(x => x.Documento.Equals(Cedula));
                if (integrante == null)
                {
                    this.integrante = new Integrante()
                    {
                        Nombre = Nombre,
                        Apellidos = Apellidos,
                        Documento = Cedula,
                        Nacionalidad = Nacionalidad,
                        Sexo = Sexo,
                        Cvlac = Cvlac,
                        NivelAcademico = Convert.ToDecimal(idNivelAcademico),
                        Correo = Correo,
                        TipoIntegrante = Convert.ToDecimal(idTipoIntegrante),
                        FechaIngreso = Convert.ToDateTime(FechaIngreso),
                        FechaUpdate = DateTime.Now,
                        Estado = 3
                    };

                    bd.Integrante.Add(integrante);
                    bd.SaveChanges();

                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Operacion exitosa";
                    ViewBag.MessageMSG = "Se a creado el Integrante " + Nombre + " " + Apellidos + " de manera exitosa";
                    ViewBag.IconMSG = "success";
                    return View();
                }
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Operacion invalida";
                ViewBag.MessageMSG = "El documento " + Cedula + " ya se encuentra registrado en el sistema";
                ViewBag.IconMSG = "error";
            }
            return View();
        }



        public List<NivelAcademico> listarNivelesAcademicos()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                return bd.NivelAcademico.Where(x => x.Estado1.Nombre.Equals("Activo")).ToList();
            }

        }

        public List<TipoIntegrante> listarTiposIntegrantes()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                return bd.TipoIntegrante.Where(x => x.Estado1.Nombre.Equals("Activo")).ToList();
            }
        }



    }
}