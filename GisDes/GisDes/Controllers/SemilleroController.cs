using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GisDes.Models;
using System.Diagnostics;

namespace GisDes.Controllers
{
    /// <summary>
    /// Clase encargada de manejar varias funciones del semillero
    /// Version: 1.0
    /// 
    /// </summary>
    public class SemilleroController : Controller
    {
        private GisdesEntity bd = new GisdesEntity();

        /// <summary>
        /// Método que trae en una lista con la información del Semillero
        /// Autor: Carolina Quintero Valencia
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarSemillero()
        {
            GisdesEntity db = new GisdesEntity();
            return View(db.SemilleroInvestigacion.Where(Semillero => Semillero.Estado1.Nombre.Equals("Activo")).ToList());
        }

        public ActionResult Index()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                IntegranteYsemillero integranteYsemillero = new IntegranteYsemillero();
                integranteYsemillero.semilleroInvestigacion = bd.SemilleroInvestigacion.ToList();

                return View(integranteYsemillero);

            }
        }
        /*
         * autor Cristian Camilo Buitrago
         * se muestra el nombre que le llega al metodo y busca el semillero por nombre que le entra como parametro
         * y se llama al metodo ListarIntegrantesNombres y le envia el id
         */
        [HttpPost]
        public ActionResult Index(decimal SelectorSemillero)
        {

            using (GisdesEntity bd = new GisdesEntity())
            {
                IntegranteYsemillero integranteYsemillero = ListarIntegrantesNombres(SelectorSemillero);
                integranteYsemillero.semilleroInvestigacion = bd.SemilleroInvestigacion.ToList();

                return View(integranteYsemillero);

            }


        }


        /*
         * autor Cristian Camilo Buitrago 
         * se buscan todos los integranne que pertenescan al semillero y se envian a la vista 
         * para formr la tabla
         */
        private IntegranteYsemillero ListarIntegrantesNombres(decimal id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                decimal idSemillero = id;
                List<IntegranteSemilleroInvestigacion> idsintegrantes = bd.IntegranteSemilleroInvestigacion.Where(IntegranteSemilleroInvestigacion => IntegranteSemilleroInvestigacion.IdSemillero == idSemillero).ToList();
                List<Integrante> integrantes = new List<Integrante>();
                foreach (var item in idsintegrantes)
                {
                    List<Integrante> inte = bd.Integrante.Where(integ => integ.Id == item.IdIntegrante).ToList();
                    integrantes.Add(inte.First());
                }
                IntegranteYsemillero integranteYsemillero = new IntegranteYsemillero()
                {
                    integrantes = integrantes,
                    semilleroInvestigacion = bd.SemilleroInvestigacion.Where(x => x.Estado == 1).ToList()
                };

                return integranteYsemillero;
            }
        }


        /// <summary>
        /// Metodo CrearSemillero realizar el registro de semilleros de investigacion en la implementacion del sistema.
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearSemillero()
        {
            Debug.WriteLine("Hola Entreeeeee");
            CrearSemillero nuevo = new CrearSemillero()
            {
                listaIntegrantesDocentes = bd.Integrante.Where(x => x.TipoIntegrante == 3 && x.Estado == 1).ToList(),
                listaLineasInvestigacion = bd.LineaInvestigacion.Where(x => x.Estado1.Nombre.Equals("activo")).ToList()

            };
            return View(nuevo);

        }


        /// <summary>
        /// Metodo que recibe los datos desde la vista para realizar el registro de un nuevo semillero
        /// </summary>
        /// <param name="nombre">Nombre del semillero a crear</param>
        /// <param name="coordinador">Coordinador del semillero</param>
        /// <param name="lineaInvestigacion">Linea de investigacion a la que pertenece</param>
        /// <param name="fechaCreacion">Fecha en al fue creado el semillero</param>
        /// <param name="enlace">Link web del semillero de investigacion</param>
        /// <param name="objGeneral">Objetivo general del semilleros</param>
        /// <param name="objEspecificos">Objetivos especificos del semillero</param>
        /// <param name="tags">Palabras claves para identificar el semillero de investigacion</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearSemillero(String nombre, String coordinador, string lineaInvestigacion, string fechaCreacion, string enlace, string objGeneral, string objEspecificos, string tags)
        {
            string nombreCoordinador = coordinador.Split(' ')[1].ToUpper();
            string apellidoCoordinador = coordinador.Split(' ')[2].ToUpper();
            Integrante coordinadorSemillero = bd.Integrante.Where(x => x.Nombre.ToUpper().Equals(nombreCoordinador) && x.Apellidos.ToUpper().Equals(apellidoCoordinador)).ToList()[0];
            SemilleroInvestigacion semillero = new SemilleroInvestigacion()
            {
                Nombre = nombre,
                Coordinador = coordinadorSemillero.Id,
                LineaInvestigacion1 = bd.LineaInvestigacion.Where(x => x.Nombre.ToUpper().Equals(lineaInvestigacion.ToUpper())).ToList()[0],
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Enlace = enlace,
                ObjetivoGeneral = objGeneral,
                ObjetivosEspecificos = objEspecificos,
                Tags = tags,
                FechaUpdate = Convert.ToDateTime(fechaCreacion),
                Estado = 1
            };

            bd.SemilleroInvestigacion.Add(semillero);
            bd.SaveChanges();

            ViewBag.viewMessage = true;
            ViewBag.TitleMSG = "Operacion exitosa";
            ViewBag.MessageMSG = "Se a creado el semillero" + nombre + " de manera exitosa";
            ViewBag.IconMSG = "success";


            CrearSemillero nuevo = new CrearSemillero()
            {
                listaIntegrantesDocentes = bd.Integrante.Where(x => x.TipoIntegrante == 3 && x.Estado == 1).ToList(),
                listaLineasInvestigacion = bd.LineaInvestigacion.Where(x => x.Estado1.Nombre.Equals("activo")).ToList()

            };
            return View(nuevo);

        }

        public ActionResult CambiarEstadoSemillero()
        {
            CambiarEstadoSemillero estado = new CambiarEstadoSemillero();

            if (estado.listaSemilleros().Count > 0)
            {
                if (estado.listaEstados().Count > 0)
                {
                    return View(estado);
                }
                else
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "No existen Estados en la base de datos, consulte al administrador.";
                    ViewBag.IconMSG = "error";
                }
            }

            else
            {
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No existen semilleros en la base de datos, consulte al administrador.";
                ViewBag.IconMSG = "error";
            }
            return View(estado);
        }

        [HttpPost]
        public ActionResult CambiarEstadoSemillero(decimal idSemillero, decimal estado)
        {
            CambiarEstadoSemillero CambiarEstado = new CambiarEstadoSemillero();
            string[] resultado = CambiarEstado.cambiarEstadoSemillero(idSemillero, estado);
            ViewBag.viewMessage = true;
            ViewBag.TitleMSG = resultado[1];
            ViewBag.MessageMSG = resultado[2];
            ViewBag.IconMSG = resultado[0];
            return View();
        }

        public ActionResult AsociarIntegranteSemillero()
        {
            AsociarIntegrante modelo = new AsociarIntegrante();
            if (modelo.listarSemilleros().Count > 0)
            {
                if (modelo.listarIntegrantes().Count > 0)
                {
                    return View(modelo);
                }
                else
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "No existen Integranes en la base de datos, consulte al administrador.";
                    ViewBag.IconMSG = "error";
                }
            }

            else
            {
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No existen semilleros en la base de datos, consulte al administrador.";
                ViewBag.IconMSG = "error";
            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult AsociarIntegranteSemillero(decimal idSemillero, decimal idIntegrante, string fecha)
        {

            return View();
        }


        /// <summary>
        /// Metodo que devuelve la interface en blanco de ConsultarSemillero
        /// Caso de uso: CU-CS1 
        /// Ultima revision: 2018-06-23
        /// Autor: Andres Eduardo Cardenas. 
        /// </summary>
        /// <returns></returns>

        public ActionResult ConsultarSemillero()
        {
            return View();
        }


        /// <summary>
        /// Metodo que devuelve la interface nueva con la informacion del semillero consultado
        /// Caso de uso: CU-CS1 
        /// Ultima revision: 2018-06-23
        /// Autor: Andres Eduardo Cardenas. 
        /// </summary>
        /// <param name="PalabraClave"></param>
        /// <param name="palabraSeleccionada"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ConsultarSemillero(String PalabraClave, String palabraSeleccionada)
        {
            SemilleroInvestigacion semilleroBuscado;
            List<SemilleroInvestigacion> semilleroBuscadoTAG;
            ConsultaSemilleroInfo semilleroConsultado = new ConsultaSemilleroInfo();
            switch (palabraSeleccionada)
            {
                case "BusquedaPorNombre":
                    semilleroBuscado = bd.SemilleroInvestigacion.Where(nombresemillero => nombresemillero.Nombre.ToUpper().Equals(PalabraClave)).ToList()[0];
                    semilleroConsultado.SemilleroInvestigacionNombre = semilleroBuscado;
                    break;

                case "BusquedaPorTag":
                    semilleroBuscadoTAG = bd.SemilleroInvestigacion.Where(tagsemillero => tagsemillero.Tags.ToUpper().Contains(PalabraClave)).ToList();
                    semilleroConsultado.SemilleroInvestigacionTag = semilleroBuscadoTAG;
                    break;
            }

            return View();
        }



        // GET: ActualizarSemillero
        public ActionResult ActualizarSemillero(decimal id)
        {
            return View(CargarInformacion(id));
        }

        public ActualizarSemillero CargarInformacion(decimal id)
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            actualizar.integrantes = CargarIntegrantes();
            actualizar.LineInvetigacion = CargarLineaInvestigacion(0);
            actualizar.semillero = ObtenerSemilleroInvestigacion(id);
            return actualizar;
        }

        [HttpPost]
        public ActionResult ActualizarSemillero(decimal id, String Nombre, decimal Coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            decimal LineaInvestigacion, String Enlace, String Tags)
        {
            Boolean a = ComprobarDatos(Nombre, Coordinador,
                ObjetivoGeneral, ObjetivoEspecifico,
                LineaInvestigacion, Enlace);
            if (a)
            {


                Boolean b = GuardarSemillero(id, Nombre, Coordinador,
                ObjetivoGeneral, ObjetivoEspecifico,
                LineaInvestigacion, Enlace, Tags);
                //return Redirect(ListaIntegrantes);
                if (b)
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Operacion exitosa";
                    ViewBag.MessageMSG = "Se actualizo el semillero ";
                    ViewBag.IconMSG = "success";
                    return RedirectToAction("ListarSemillero");//organizar
                }
                else
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "No se Pudo actualizar el semillero ";
                    ViewBag.IconMSG = "error";
                }
            }
            else
            {
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No se pede ingresar datos vacios.";
                ViewBag.IconMSG = "error";

            }
            return View(CargarInformacion(id));
        }

        public Boolean GuardarSemillero(decimal id, String Nombre, decimal Coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            decimal LineaInvestigacion, String Enlace, String Tag)
        {

            bool guarda = false;
            using (GisdesEntity bd = new GisdesEntity())
            {

                SemilleroInvestigacion semilleroGuardar = bd.SemilleroInvestigacion.Find(id); ;
                semilleroGuardar.Nombre = Nombre;
                semilleroGuardar.Coordinador = Coordinador;
                semilleroGuardar.ObjetivoGeneral = ObjetivoGeneral;
                semilleroGuardar.ObjetivosEspecificos = ObjetivoEspecifico;
                semilleroGuardar.LineaInvestigacion = LineaInvestigacion;
                semilleroGuardar.Enlace = Enlace;
                semilleroGuardar.FechaUpdate = DateTime.Now;
                semilleroGuardar.Tags = Tag;
                if (bd.SaveChanges() > 0)
                {
                    guarda = true;
                }

            }
            return guarda;
        }


        public List<Integrante> CargarIntegrantes()
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerIntegrantes();
        }

        public List<LineaInvestigacion> CargarLineaInvestigacion(decimal Codigo)
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerlineaInvestigacions();
        }

        public SemilleroInvestigacion ObtenerSemilleroInvestigacion(decimal Id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                //List<SemilleroInvestigacion> semillero =;
                return bd.SemilleroInvestigacion.Find(Id);
            }
        }

        public Boolean ComprobarDatos(String Nombre, decimal coordinador, String ObjetivoGeneral, String ObjetivoEspecifico,
            decimal LineaInvestigacion, String Enlace)
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


        //Metdos caso de uso DetalleSemillero

        public ActionResult MostrarInformacion(int id)
        {
            SemilleroDetalle Detallesemillero =new SemilleroDetalle();
            Detallesemillero.semillero = traeDetalleSemillero(id);
            Detallesemillero.NombreCoordinador = NombreIntegranteDirector((int) Detallesemillero.semillero.Coordinador);
            Detallesemillero.Estado = estadoSemillero((int)Detallesemillero.semillero.Estado);

            return View(Detallesemillero);
        }

        private String NombreIntegranteDirector(int id)
        {
            using (GisdesEntity bd = new GisdesEntity()) {
                return bd.Integrante.Find(id).Nombre.ToString();
            }
        }

        private String estadoSemillero(int id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {

                return bd.Estado.Find(id).Nombre.ToString();
            }
        }

        public SemilleroInvestigacion traeDetalleSemillero(int id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                //List<SemilleroInvestigacion> semillero =;
                return bd.SemilleroInvestigacion.Find(id);
            }
        }
    }
}
