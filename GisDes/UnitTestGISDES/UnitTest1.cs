using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GisDes.Controllers;
using GisDes.Models;
using System.Web.Mvc;

namespace UnitTestGISDES
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void listarIntegrantesNombres()
        {

            SemilleroController semilleroController = new SemilleroController();
            decimal id = 1;
            String Nombre = "";
            decimal Coordinador = 1;
            String ObjetivoGeneral = "";
            String ObjetivoEspecifico = "";
            decimal LineaInvestigacion = 1;
            String Enlace = "";
            String Tag = "";
            Boolean salidaComparar = true;

            Boolean salidasGuardarSemillero = semilleroController.GuardarSemillero(id, Nombre, Coordinador, ObjetivoGeneral, ObjetivoEspecifico,
            LineaInvestigacion, Enlace, Tag);
            Assert.AreEqual(salidasGuardarSemillero, salidaComparar);

        }

        [TestMethod]
        public void asociarIntegranteSemillero()
        {
            AsociarIntegrante asociarIntegrante = new AsociarIntegrante();
            decimal idSemillero = 1;
            decimal idIntegrante = 1;
            String fecha = "2018/05/09";
            string[] salidaAsociarIntegrante = asociarIntegrante.asociarIntegranteSemillero(idSemillero, idIntegrante, fecha);
            string[] salidaComparar = new string[3];
            salidaComparar[0] = "success";
            salidaComparar[1] = "Operacion exitosa";
            salidaComparar[2] = "Se a creado existosamente la relacion";
            Assert.AreEqual(salidaAsociarIntegrante, salidaComparar);

        }

        }

}