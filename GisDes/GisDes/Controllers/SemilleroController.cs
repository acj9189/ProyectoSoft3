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
        private bool vistaVentana = true;
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


        /// <summary>
        /// Metodo que inicialisa la pagina que lista los integrantes que pertenecen a un esmilleros
        /// Autor:Cristian Camilo Buitrago
        /// </summary>
        /// <returns></returns>se retorna los valores iniciales que tendra la pagina
        public ActionResult Index()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                
                IntegranteYsemillero integranteYsemillero = new IntegranteYsemillero();
                integranteYsemillero.semilleroInvestigacion = bd.SemilleroInvestigacion.ToList();
                if (integranteYsemillero.semilleroInvestigacion.First()==null) {
                    return View();
                }
                return View(integranteYsemillero);

            }
        }


        /// <summary>
        ///  se muestra el nombre que le llega al metodo y busca el semillero por nombre que le entra como parametro
        ///  y se llama al metodo ListarIntegrantesNombres y le envia el id
        ///  autor Cristian Camilo Buitrago
        /// </summary>
        /// <param name="SelectorSemillero"></param> entra el id del semillero del cual se mostraran sus integranntes
        /// <returns></returns>retorna la informacion que se pondra en la vista
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


      
           
         /// <summary>
         ///  se buscan todos los integranne que pertenescan al semillero y se envian a la vista 
         ///  para formr la tabla
         /// Autor: Cristian Camilo Buitrago
         /// </summary>
         /// <param name="id"></param> id del semillero al que se obtendran los integrantes
         /// <returns></returns>deuelve los integrantes del semollero
        private IntegranteYsemillero ListarIntegrantesNombres(decimal id)
        {

            using (GisdesEntity bd = new GisdesEntity())
            {
                decimal idSemillero = id;
                List<IntegranteSemilleroInvestigacion> idsintegrantes = bd.IntegranteSemilleroInvestigacion.Where(IntegranteSemilleroInvestigacion => IntegranteSemilleroInvestigacion.IdSemillero == idSemillero).ToList();
                List<Integrante> integrantes = new List<Integrante>();
                List<String> fechaIgreso = new List<string>();
                foreach (var item in idsintegrantes)
                {
                    List<Integrante> inte = bd.Integrante.Where(integ => integ.Id == item.IdIntegrante).ToList();
                    inte.First().FechaIngreso = item.FechaIngreso;
                    integrantes.Add(inte.First());

                }
                IntegranteYsemillero integranteYsemillero = new IntegranteYsemillero()
                {
                    integrantes = integrantes,
                    semilleroInvestigacion = bd.SemilleroInvestigacion.Where(x => x.Estado == 1).ToList(),
                    FechaIngreso = fechaIgreso
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
            CrearSemillero nuevo = new CrearSemillero()
            {
                listaIntegrantesDocentes = bd.Integrante.Where(x => x.TipoIntegrante1.Nombre.Equals("Profesor") && x.Estado1.Nombre.Equals("Activo")).ToList(),
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
            string linea = lineaInvestigacion.Split('-')[0].Trim().ToUpper();
            Integrante coordinadorSemillero = bd.Integrante.Where(x => x.Nombre.ToUpper().Equals(nombreCoordinador) && x.Apellidos.ToUpper().Equals(apellidoCoordinador)).ToList()[0];
            Estado estadoD = bd.Estado.Where(x => x.Nombre.Equals("Activo")).ToList()[0];
            LineaInvestigacion lineaI = bd.LineaInvestigacion.Where(x => x.Nombre.ToUpper().Equals(linea)).ToList()[0];

            SemilleroInvestigacion semillero = new SemilleroInvestigacion()
            {
                Nombre = nombre,
                Coordinador = coordinadorSemillero.Id,
                LineaInvestigacion = lineaI.Id,
                FechaCreacion = Convert.ToDateTime(fechaCreacion),
                Enlace = enlace,
                ObjetivoGeneral = objGeneral,
                ObjetivosEspecificos = objEspecificos,
                Tags = tags,
                FechaUpdate = Convert.ToDateTime(fechaCreacion),
                Estado = estadoD.Id
            };

            bd.SemilleroInvestigacion.Add(semillero);
            bd.SaveChanges();

/*            ViewBag.viewMessage = true;
            ViewBag.TitleMSG = "Operacion exitosa";
            ViewBag.MessageMSG = "Se a creado el semillero" + nombre + " de manera exitosa";
            ViewBag.IconMSG = "success";*/
            PonerInformacionEnVEntana("Operacion exitosa", "Se a creado el semillero" + nombre + " de manera exitosa", "success");

            CrearSemillero nuevo = new CrearSemillero()
            {
                listaIntegrantesDocentes = bd.Integrante.Where(x => x.TipoIntegrante1.Nombre.Equals("Profesor") && x.Estado1.Nombre.Equals("Activo")).ToList(),
                listaLineasInvestigacion = bd.LineaInvestigacion.Where(x => x.Estado1.Nombre.Equals("activo")).ToList()

            };
            return View(nuevo);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                    /*ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "No existen Estados en la base de datos, consulte al administrador.";
                    ViewBag.IconMSG = "error";
                    */
                    PonerInformacionEnVEntana("Error", "No existen Estados en la base de datos, consulte al administrador.", "error");

                }
            }

            else
            {
               /* ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No existen semilleros en la base de datos, consulte al administrador.";
                ViewBag.IconMSG = "error";*/

                PonerInformacionEnVEntana("Error", "No existen semilleros en la base de datos, consulte al administrador.", "error");

            }
            return View(estado);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSemillero"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo AsociarIntegranteSemillero crea la relacion entre integrante y semillero a partir de una fecha
        /// METODO GET
        /// </summary>
        /// <returns></returns>
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
                    /* ViewBag.viewMessage = true;
                     ViewBag.TitleMSG = "Error";
                     ViewBag.MessageMSG = "No existen Integranes en la base de datos, consulte al administrador.";
                     ViewBag.IconMSG = "error";*/

                    PonerInformacionEnVEntana("Error", "No existen Integranes en la base de datos, consulte al administrador.", "error");
                }
            }

            else
            {
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No existen semilleros en la base de datos, consulte al administrador.";
                ViewBag.IconMSG = "error";

                PonerInformacionEnVEntana("Error", "No existen semilleros en la base de datos, consulte al administrador.", "error");

            }
            return View(modelo);
        }

        /// <summary>
        /// Descripcion: Metodo AsociarIntegranteSemillero crea la relacion entre integrante y semillero as partir de una fecha
        /// METODO POST
        /// </summary>
        /// <param name="idSemillero">id del semillero al que va a pertenecer el integrante</param>
        /// <param name="idIntegrante">id del integrante</param>
        /// <param name="fecha">fecha en la que se asocia al semillero</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AsociarIntegranteSemillero(decimal idSemillero, decimal idIntegrante, string fecha)
        {
            AsociarIntegrante modelo = new AsociarIntegrante();
            Estado estado = bd.Estado.Where(x => x.Nombre.Equals("Activo")).ToList()[0];
            IntegranteSemilleroInvestigacion integrante = new IntegranteSemilleroInvestigacion()
            {
                IdIntegrante = idIntegrante,
                IdSemillero = idSemillero,
                FechaIngreso = Convert.ToDateTime(fecha),
                FechaUpdate = DateTime.Now,
                Estado = estado.Id
            };
            bd.IntegranteSemilleroInvestigacion.Add(integrante);
            bd.SaveChanges();

            /*
            ViewBag.viewMessage = true;
            ViewBag.TitleMSG = "Operacion exitosa";
            ViewBag.MessageMSG = "Se a asociado correctamente el nuevo integrante";
            ViewBag.IconMSG = "success";*/


            

            PonerInformacionEnVEntana("Operacion exitosa", "Se a asociado correctamente el nuevo integrante", "success");

            return View(modelo);
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
        /// <summary>
        /// Metodo que carga la vista de actualiza un semillero
        /// Autor: Cristian CAmilo Buitrago
        /// </summary>
        /// <param name="id"></param> id del cemilero que se desea actualizar la vista
        /// <returns></returns> retorna la vista del semillero
        public ActionResult ActualizarSemillero(decimal id)
        {
            return View(CargarInformacion(id));
        }

        /// <summary>
        /// Metodo que busca la informacion del semillero 
        /// </summary>
        /// <param name="id"></param> id del semillero que se quiere cambiar la informacion
        /// <returns></returns> debuelve el semillero con toda su informacion
        public ActualizarSemillero CargarInformacion(decimal id)
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            actualizar.integrantes = CargarIntegrantes();
            actualizar.LineInvetigacion = CargarLineaInvestigacion(0);
            actualizar.semillero = ObtenerSemilleroInvestigacion(id);
            return actualizar;
        }

        /// <summary>
        /// recive la nueva informacion del semillero y la cambia por la antigua informacion 
        /// 
        /// Autor:Cristian Camilo Buitrago
        /// </summary>
        /// <param name="id"></param> id del al que se desea actualzar la informacion.
        /// <param name="Nombre"></param>Nombre del semillero
        /// <param name="Coordinador"></param>numero que identifica al semillero
        /// <param name="ObjetivoGeneral"></param> objetivo general de semillero
        /// <param name="ObjetivoEspecifico"></param>objetivo especifico del semillero
        /// <param name="LineaInvestigacion"></param>numero que iddentifica la linea de investigacion
        /// <param name="Enlace"></param>enlace a la pagina de la linea de investifacion
        /// <param name="Tags"></param> tgas por los que se puede encontrar el semillero
        /// <returns></returns> 
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
                   /* ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Operacion exitosa";
                    ViewBag.MessageMSG = "Se actualizo el semillero";
                    ViewBag.IconMSG = "success";
                    */
                    PonerInformacionEnVEntana("Operacion exitosa", "Se actualizo el semillero", "success");
                    return RedirectToAction("ListarSemillero");//organizar
                }
                else
                {
                   /* ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "No se Pudo actualizar el semillero ";
                    ViewBag.IconMSG = "error";*/
                    PonerInformacionEnVEntana("Error", "No se Pudo actualizar el semillero ", "error");
                }
            }
            else
            {
               /* ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "No se pede ingresar datos vacios.";
                ViewBag.IconMSG = "error";*/
                PonerInformacionEnVEntana("Error", "No se Pudo actualizar el semillero ", "error");

            }
            return View(CargarInformacion(id));
        }


        private void PonerInformacionEnVEntana(String tituloVentana,String mensajeVentana, String Icocno)
        {
            ViewBag.viewMessage = this.vistaVentana;
            ViewBag.TitleMSG = tituloVentana;
            ViewBag.MessageMSG = mensajeVentana;
            ViewBag.IconMSG = Icocno;
        }

        /// <summary>
        /// metodo que guarda la informacion en el la base de datos
        /// Autor: Cristian CAmilo Buitrago
        /// </summary>
        /// <param name="id"></param> id del al que se desea actualzar la informacion.
        /// <param name="Nombre"></param>Nombre del semillero
        /// <param name="Coordinador"></param>numero que identifica al semillero
        /// <param name="ObjetivoGeneral"></param> objetivo general de semillero
        /// <param name="ObjetivoEspecifico"></param>objetivo especifico del semillero
        /// <param name="LineaInvestigacion"></param>numero que iddentifica la linea de investigacion
        /// <param name="Enlace"></param>enlace a la pagina de la linea de investifacion
        /// <param name="Tags"></param> tgas por los que se puede encontrar el semillero
        /// <returns></returns>
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

        /// <summary>
        /// debuelve una lista de integrantes que pertenecen al semillero
        /// </summary>
        /// <returns></returns>
        public List<Integrante> CargarIntegrantes()
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerIntegrantes();
        }


        /// <summary>
        /// Metodo que carga la linea de investigacion del semillero
        /// </summary>
        /// <param name="Codigo"></param> codigo con el que se identifica el semillero dentro de la base de datos
        /// <returns></returns>
        public List<LineaInvestigacion> CargarLineaInvestigacion(decimal Codigo)
        {
            ActualizarSemillero actualizar = new ActualizarSemillero();
            return actualizar.ObtenerlineaInvestigacions();
        }


        /// <summary>
        /// Metodo que carga la informacion del semillero
        /// Autor: Cristian Camilo Buitrago
        /// </summary>
        /// <param name="Id"></param> numero con el que se identifica un semillero
        /// <returns></returns>
        public SemilleroInvestigacion ObtenerSemilleroInvestigacion(decimal Id)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                //List<SemilleroInvestigacion> semillero =;
                return bd.SemilleroInvestigacion.Find(Id);
            }
        }


        /// <summary>
        /// Metodo que verifica si los datos ingresados contienen informacion 
        /// </summary>
        /// <param name="Nombre"></param> nombre del semillero
        /// <param name="coordinador"></param> numero con el que se identifica el coordinador en la bse de datos
        /// <param name="ObjetivoGeneral"></param> objetivo general del semillero
        /// <param name="ObjetivoEspecifico"></param>objetivo especifico del semillero
        /// <param name="LineaInvestigacion"></param> numero con el que identifica la linea de investigacion dentro de la base de datos
        /// <param name="Enlace"></param> enlace de la pagina de este semillero
        /// <returns></returns>
        public Boolean ComprobarDatos(String Nombre, decimal coordinador, String ObjetivoGeneral, String ObjetivoEspecifico, decimal LineaInvestigacion, String Enlace)
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

        public ActionResult listarActividadesSemilleros()
        {
            return View(new List<ActividadAsociadoSemilleroInv>());
        }

        [HttpPost]
        public ActionResult listarActividadesSemilleros(decimal idSemillero)
        {
            GisdesEntity bd = new GisdesEntity();
            List<ActividadAsociadoSemilleroInv> lista = bd.ActividadAsociadoSemilleroInv.Where(x => x.IdSemillero == idSemillero).ToList();
            if(lista.Count == 0)
            {
               /* ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Info";
                ViewBag.MessageMSG = "El semillero elegido no posee actividades";
                ViewBag.IconMSG = "info";*/

                PonerInformacionEnVEntana("Info", "El semillero elegido no posee actividades", "info");
            }
            return View(lista);
        }
    }
}
