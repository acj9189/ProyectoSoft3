using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class SolicitudsController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/Solicituds
        public IQueryable<Solicitud> GetSolicitud()
        {
            return db.Solicitud;
        }

        // GET: api/Solicituds/5
        [ResponseType(typeof(Solicitud))]
        public async Task<IHttpActionResult> GetSolicitud(decimal id)
        {
            Solicitud solicitud = await db.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return Ok(solicitud);
        }

        // GET: api/Solicituds/5
        [ResponseType(typeof(Solicitud))]
        public async Task<IHttpActionResult> GetSolicitud(string NombreIntegrante, string correo, string descripcion, string Idintegrante, string IdSemillero, string IdCoordinador)
        {
            SqlConnection conexionBaseDatos = new SqlConnection("Data Source=software220181.database.windows.net; " +
                "Initial Catalog=GISDES;" +
                "User ID=software2;" +
                "Password=sistemas.2018;");
            SqlDataAdapter cantidadId = new SqlDataAdapter("SELECT id FROM SOLICITUD", conexionBaseDatos);
            DataSet toalId = new DataSet();
            cantidadId.Fill(toalId);
            int idSolicitud = toalId.Tables[0].Rows.Count + 1;

            string id = idSolicitud.ToString();
            

            SqlDataAdapter da = new SqlDataAdapter("SET IDENTITY_INSERT SOLICITUD on INSERT INTO SOLICITUD (NombreIntegrante,correo," +
                "DESCRIPCIONPORQUEQUIEREINGRESAR,IdIntegrante,IDSEMoLLEROinvestigacion,COORDINADOR,ID)" +
                "VALUES(" + "'" + NombreIntegrante + "'" + "," + "'" + correo + "'" + "," + "'" + descripcion + "'" + "," + Idintegrante + "," +
                IdSemillero + "," + IdCoordinador + "," + id + ")", conexionBaseDatos);
            DataSet ds = new DataSet();
            da.Fill(ds);

            hacerNotificacion(id, IdCoordinador, "NO atendido", conexionBaseDatos);
            return null;
        }

        // GET: api/Solicituds/5
        [ResponseType(typeof(Solicitud))]
        public async Task<IHttpActionResult> GetSolicitud(string dato)
        {
            SqlConnection conexionBaseDatos = new SqlConnection("Data Source=software220181.database.windows.net; " +
                "Initial Catalog=GISDES;" +
                "User ID=software2;" +
                "Password=sistemas.2018;");

            string[] datos = dato.Split('<');

            //System.Diagnostics.Debug.WriteLine("ENTRO .... ++++ " + dato);
            string NombreIntegrante = datos[0];
            string correo = datos[1];
            string descripcion = datos[2];
            string Idintegrante = datos[3];
            string IdSemillero = datos[4];
            string IdCoordinador = datos[5];

            SqlDataAdapter cantidadId = new SqlDataAdapter("SELECT id FROM SOLICITUD", conexionBaseDatos);
            DataSet toalId = new DataSet();
            cantidadId.Fill(toalId);
            int idSolicitud = toalId.Tables[0].Rows.Count + 1;

            string id = idSolicitud.ToString();
   
            SqlDataAdapter da = new SqlDataAdapter("SET IDENTITY_INSERT SOLICITUD on INSERT INTO SOLICITUD (NombreIntegrante,correo," +
                "DESCRIPCIONPORQUEQUIEREINGRESAR,IdIntegrante,IDSEMoLLEROinvestigacion,COORDINADOR,ID)" +
                "VALUES(" + "'" + NombreIntegrante + "'" + "," + "'" + correo + "'" + "," + "'" + descripcion + "'" + "," + Idintegrante + "," +
                IdSemillero + "," + IdCoordinador + "," + id + ")", conexionBaseDatos);
            DataSet ds = new DataSet();
            da.Fill(ds);

            hacerNotificacion(id, IdCoordinador, "NO atendido", conexionBaseDatos);


            return null;
        }

        private void hacerNotificacion(string idSolicitud, string idCoordinador, string Estado, SqlConnection conexionBaseDatos) {

            SqlDataAdapter cantidadId = new SqlDataAdapter("SELECT IdNotificacion FROM NOTIFICACION", conexionBaseDatos);
            DataSet toalId = new DataSet();
            cantidadId.Fill(toalId);
            int idNotifacion = toalId.Tables[0].Rows.Count + 1;

            string id = idNotifacion.ToString();

            SqlDataAdapter da = new SqlDataAdapter("INSERT INTO NOTIFICACION (IdNotificacion, IdSolicitud, idCoordinador," +
              "EstadoRegistro)" +
              "VALUES(" + "'" + id + "'" + ","  + "'" + idSolicitud + "'" + "," + "'" + idCoordinador + "'" + "," + "'" + Estado + "'" + ")", conexionBaseDatos);
            DataSet ds = new DataSet();
            da.Fill(ds);


        }

        //// PUT: api/Solicituds/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutSolicitud(decimal id, Solicitud solicitud)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != solicitud.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(solicitud).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SolicitudExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Solicituds
        //[ResponseType(typeof(Solicitud))]
        //public async Task<IHttpActionResult> PostSolicitud(Solicitud solicitud)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Solicitud.Add(solicitud);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SolicitudExists(solicitud.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = solicitud.Id }, solicitud);
        //}


        //// POST: api/Solicituds 
        //[ResponseType(typeof(Solicitud))]
        //public async Task<IHttpActionResult> PostSolicitud(string datos)
        //{
        //    string[] a = datos.Split(',');
        //    Solicitud solicitud = new Solicitud();
        //  //  solicitud.Id = id;
        //    solicitud.Correo = a[0];
        //    solicitud.Coordinador =  Convert.ToDecimal(a[1]);
        //    solicitud.IdIntegrante = Convert.ToDecimal(a[2]);
        //    solicitud.NombreIntegrante = a[3];
        //    solicitud.DescripcionPorqueQuiereIngresar = a[4];
        //    solicitud.IdSemolleroInvestigacion = Convert.ToDecimal(a[5]);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Solicitud.Add(solicitud);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SolicitudExists(solicitud.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = solicitud.Id }, solicitud);
        //}

        //// DELETE: api/Solicituds/5
        //[ResponseType(typeof(Solicitud))]
        //public async Task<IHttpActionResult> DeleteSolicitud(decimal id)
        //{
        //    Solicitud solicitud = await db.Solicitud.FindAsync(id);
        //    if (solicitud == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Solicitud.Remove(solicitud);
        //    await db.SaveChangesAsync();

        //    return Ok(solicitud);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool SolicitudExists(decimal id)
        {
            return db.Solicitud.Count(e => e.Id == id) > 0;
        }
    }
}