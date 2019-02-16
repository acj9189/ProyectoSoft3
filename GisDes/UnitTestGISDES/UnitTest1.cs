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
        public void testlistarIntegrantesNombresPositivo()
        {
            using (GisdesEntity bd = new GisdesEntity())
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
                Assert.Equals(salidasGuardarSemillero, salidaComparar);
            }

        }

        [TestMethod]
        public void testlistarIntegrantesNombresNegativo()
        {
            using (GisdesEntity bd = new GisdesEntity())
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
                // Boolean salidaComparar = false;

                Boolean salidasGuardarSemillero = semilleroController.GuardarSemillero(id, Nombre, Coordinador, ObjetivoGeneral, ObjetivoEspecifico,
                LineaInvestigacion, Enlace, Tag);
                Assert.IsFalse(salidasGuardarSemillero);

            }

           

        }

        [TestMethod]
        public void testlistarIntegrantesNombresExeption()
        {
            using (GisdesEntity bd = new GisdesEntity())
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
                Boolean salidaComparar = false;

                Boolean salidasGuardarSemillero = semilleroController.GuardarSemillero(id, Nombre, Coordinador, ObjetivoGeneral, ObjetivoEspecifico,
                LineaInvestigacion, Enlace, Tag);
                Assert.IsNull(salidasGuardarSemillero);
            }

        }



        [TestMethod]
        public void asociarIntegranteSemillero()
        {
            using (GisdesEntity bd = new GisdesEntity())
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

       

        [TestMethod]
        public void comprobarDatos()
        {
            using (GisdesEntity bd = new GisdesEntity())
            {
                ActualizarSemilleroController comprobarDatos = new ActualizarSemilleroController();
                string nombre = "";
                int coordinador = 1;
                string objetivoGeneral = "";
                string objetivoEspecifico = "";
                int lineaInvestigacion = 1;
                string enlace = "";
                Boolean comprobarDatosEstado;
                comprobarDatosEstado = comprobarDatos.ComprobarDatos(nombre, coordinador, objetivoGeneral, objetivoEspecifico, lineaInvestigacion, enlace);
                Assert.IsTrue(comprobarDatosEstado);

            }


        }

    }
}