using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GisDes.Models
{
    public class AsociarIntegrante
    {

        public List<Integrante> listarIntegrantes()
        {
            List<Integrante> lista = new List<Integrante>();
            using (GisdesEntity bd = new GisdesEntity())
            {
                lista = bd.Integrante.Where(x => x.Estado1.Nombre.Equals("Activo")).ToList();
            }
            return lista;
        }

        public List<SemilleroInvestigacion> listarSemilleros()
        {
            List<SemilleroInvestigacion> lista = new List<SemilleroInvestigacion>();
            using (GisdesEntity bd = new GisdesEntity())
            {
                lista = bd.SemilleroInvestigacion.Where(x => x.Estado1.Nombre.Equals("Activo")).ToList();
            }
            return lista;
        }

        public string [] asociarIntegranteSemillero(decimal idSemillero, decimal idIntegrante, string fecha)
        {
            string[] salida = new string[3];
            using(GisdesEntity bd = new GisdesEntity())
            {
                IntegranteSemilleroInvestigacion relacion = (IntegranteSemilleroInvestigacion) bd.IntegranteSemilleroInvestigacion.Where(x => 
                        x.IdIntegrante == idIntegrante && 
                        x.IdSemillero == idSemillero &&
                        x.FechaIngreso == Convert.ToDateTime(fecha));
                if(relacion == null)
                {
                    bd.IntegranteSemilleroInvestigacion.Add(new IntegranteSemilleroInvestigacion()
                    {
                        IdSemillero = idSemillero,
                        IdIntegrante = idIntegrante,
                        FechaIngreso = Convert.ToDateTime(fecha),
                        FechaUpdate = DateTime.Today,
                        Estado = 1
                    });

                    bd.SaveChanges();
                    salida[0] = "success";
                    salida[1] = "Operacion exitosa";
                    salida[2] = "Se a creado existosamente la relacion";
                }
                else
                {
                    salida[0] = "error";
                    salida[1] = "Error";
                    salida[2] = "La relacion ya existe, Consulte con el administrador si presenta dificultades";
                }
            }
            return salida;
        }

        public List<Actividad> listarActividadesSegunSemillero(decimal idSemillero) {

            using (GisdesEntity bd = new GisdesEntity())
            {
               List<Actividad> actividades = new List<Actividad>();
               List<ActividadAsociadoSemilleroInv> acts =  bd.ActividadAsociadoSemilleroInv.Where(x => x.IdSemillero == idSemillero).ToList();
               foreach (ActividadAsociadoSemilleroInv i in acts )
               {
                    Actividad actividadSemillero = (Actividad) bd.Actividad.Where(x => x.Id ==  i.IdActividad) ;
                    actividades.Add(actividadSemillero);     
               }
                return actividades;
            } 
            
        }





    }
}