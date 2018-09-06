using GisDes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace GisDes.Controllers
{
    public class ReportesController : Controller
    {

        private SortedDictionary<string, SortedDictionary<string, int[]>> modelo = new SortedDictionary<string, SortedDictionary<string, int[]>>();
        /// <summary>
        /// Metodo que carga la vista al usuario al ser llamado
        /// Method: GET
        /// Caso de uso: CU-01 
        /// Ultima revision: 2018-05-09
        /// Autor: Jesus Daniel Gomez G.
        /// </summary>
        /// <returns></returns>
        public ActionResult ReporteSemillerosNumIntegrantes()
        {
            return View();
        }

        

        /// <summary>
        /// Metodo que calcula el porcentaje de crecimiento de los semilleros a travez del parametro de entrada
        /// Caso de uso: CU-01 
        /// Ultima revision: 2018-05-09
        /// Autor: Jesus Daniel Gomez G. 
        /// </summary>
        /// <param name="modelo"></param>
        private void CalcularPorcentajeCrecimientoSemilleros()
        {
            if (this.modelo != null && this.modelo.Count > 0)
            {
                foreach (KeyValuePair<string, SortedDictionary<string, int[]>> semillero in modelo)
                {
                    int acumulado = 0;
                    foreach (KeyValuePair<string, int[]> parametro in modelo[semillero.Key])
                    {
                        if (acumulado > 0)
                        {
                            parametro.Value[1] = (parametro.Value[0] * 100) / acumulado;
                            acumulado += parametro.Value[0];
                        }
                        else
                        {
                            parametro.Value[1] = 100;
                            acumulado += parametro.Value[0];
                        }

                    }

                }
            }
        }

        /// <summary>
        /// Metodo que genera el roporte de los semilleros segun el rango de fecha
        /// Method: POST
        /// Caso de uso: CU-01 
        /// Ultima revision: 2018-05-09
        /// Autor: Jesus Daniel Gomez G. 
        /// </summary>
        /// <param name="finicial">fecha inicial elegida</param>
        /// <param name="ffinal">fecha final elegida</param>
        /// <param name="parametro">parametro de busqueda (año, mes, dia)</param>
        /// <returns>un objeto de tipo diccionario con los datos de la consulta</returns>
        [HttpPost]
        public ActionResult ReporteSemillerosNumIntegrantes(string finicial, string ffinal, string parametro)
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                ViewBag.fechaI = finicial;
                ViewBag.fechaF = ffinal;

                DateTime fechaInicial = Convert.ToDateTime(finicial);
                DateTime fechaFinal = Convert.ToDateTime(ffinal);

                if (fechaInicial > fechaFinal)
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Error";
                    ViewBag.MessageMSG = "La fecha inicial no puede ser superior a la fecha final";
                    ViewBag.IconMSG = "error";
                    return View();
                }

                
                List<IntegranteSemilleroInvestigacion> listaSemillerosIntegrantes = bd.IntegranteSemilleroInvestigacion.Where(
                                                       integrante =>
                                                       integrante.FechaIngreso >= fechaInicial
                                                       && integrante.FechaIngreso < fechaFinal
                                                       && integrante.Estado == 1).ToList();

                foreach (IntegranteSemilleroInvestigacion semillero in listaSemillerosIntegrantes)
                {
                    if (modelo.ContainsKey(semillero.SemilleroInvestigacion.Nombre))
                    {
                        switch (parametro.ToUpper())
                        {
                            case "AÑO":
                                if (modelo[semillero.SemilleroInvestigacion.Nombre].ContainsKey(semillero.FechaIngreso.Year.ToString()))
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Year.ToString()][0] =
                                        modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Year.ToString()][0] + 1;
                                }
                                else
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre].Add(semillero.FechaIngreso.Year.ToString(), new int[] { 1, 0 });
                                }
                                break;
                            case "MES":
                                if (modelo[semillero.SemilleroInvestigacion.Nombre].ContainsKey(semillero.FechaIngreso.Month.ToString()))
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Month.ToString()][0] =
                                        modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Month.ToString()][0] + 1;
                                }
                                else
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre].Add(semillero.FechaIngreso.Month.ToString(), new int[] { 1, 0 });
                                }
                                break;
                            case "DIA":
                                if (modelo[semillero.SemilleroInvestigacion.Nombre].ContainsKey(semillero.FechaIngreso.Day.ToString()))
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Day.ToString()][0] =
                                        modelo[semillero.SemilleroInvestigacion.Nombre][semillero.FechaIngreso.Day.ToString()][0] + 1;
                                }
                                else
                                {
                                    modelo[semillero.SemilleroInvestigacion.Nombre].Add(semillero.FechaIngreso.Day.ToString(), new int[] { 1, 0 });
                                }
                                break;
                        }
                    }
                    else
                    {
                        SortedDictionary<string, int[]> datos = new SortedDictionary<string, int[]>();
                        switch (parametro.ToUpper())
                        {
                            case "AÑO":
                                datos.Add(semillero.FechaIngreso.Year.ToString(), new int[] { 1, 0 });
                                break;
                            case "MES":
                                datos.Add(semillero.FechaIngreso.Month.ToString(), new int[] { 1, 0 });
                                break;
                            case "DIA":
                                datos.Add(semillero.FechaIngreso.Day.ToString(), new int[] { 1, 0 });
                                break;
                        }
                        modelo.Add(semillero.SemilleroInvestigacion.Nombre, datos);
                    }
                }
                if (modelo.Count == 0)
                {
                    ViewBag.viewMessage = true;
                    ViewBag.TitleMSG = "Informacion";
                    ViewBag.MessageMSG = "No se encuentran registros de integrantes registrados en el rango de fechas establecido";
                    ViewBag.IconMSG = "info";

                    return View();
                }
                CalcularPorcentajeCrecimientoSemilleros();
                return View(modelo);
            }

        }

        /// <summary>
        /// Metodo que retorna el archivo en excel de los datos obtenidos en la consulta
        /// Method: POST
        /// Caso de uso: CU-01 
        /// Ultima revision: 2018-05-15
        /// Autor: Andrés Eduardo Cárdenas G.
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerarExcel(String dato)
        {
            /*if (id.ToUpper().Equals("GENERAREXCEL") && modelo == null)
            {
                System.Diagnostics.Debug.WriteLine("Entro por aqui... A");
                ViewBag.viewMessage = true;
                ViewBag.TitleMSG = "Error";
                ViewBag.MessageMSG = "Para exportar a excel antes debe generar el reporte";
                ViewBag.IconMSG = "error";
                return RedirectToAction("ReporteSemillerosNumIntegrantes", "Reportes");
            }
            else
            {*/

            this.modelo = ViewBag.modeloQ;

            String nombreDelArchivo = "ReportePorNumeroIntegrantes.xls";
            if (modelo != null || this.modelo.Count > 0 ) {
                        System.Diagnostics.Debug.WriteLine("Entro por aqui... B");

                DataTable table = new DataTable();

                table.Columns.Add("CategoryID", typeof(string));
                table.Columns.Add("Parametro", typeof(string));
                table.Columns.Add("Numero Integrantes", typeof(string));
                table.Columns.Add("% de Crecimiento", typeof(string));

                //foreach (KeyValuePair<string, SortedDictionary<string, int[]>> semillero in modelo){
                //    foreach (KeyValuePair<string, int[]> parametro in modelo[semillero.Key]) {
                //        System.Diagnostics.Debug.WriteLine(semillero.Key + parametro.Key +parametro.Value[0] +parametro.Value[1] + "%");
                //        table.Rows.Add(semillero.Key, parametro.Key, parametro.Value[0], parametro.Value[1] + "%");
                //    }
                //}

                //var gv = new GridView();
                
                //gv.DataSource = table;
                //gv.DataBind();
                //Response.ClearContent();
                //Response.Buffer = true;
                //Response.AddHeader("content-disposition", "attachment; filename=" + nombreDelArchivo);
                //Response.ContentType = "application/ms-excel";
                //Response.Charset = "";
                //StringWriter objStringWriter = new StringWriter();
                //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                //gv.RenderControl(objHtmlTextWriter);
                //Response.Output.Write(objStringWriter.ToString());
                //Response.Flush();
                //Response.End();

                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = table;
                dgGrid.DataBind();
                // Get the HTML for the control.
                dgGrid.RenderControl(hw);
                Response.ContentType = "application/xls";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreDelArchivo + "");
                Response.Write(tw.ToString());
                Response.End();


            }

           // }
            return RedirectToAction("ReporteSemillerosNumIntegrantes", "Reportes");

        }

    }
}