using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // PUT: api/Solicituds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSolicitud(decimal id, Solicitud solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitud.Id)
            {
                return BadRequest();
            }

            db.Entry(solicitud).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

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


        // POST: api/Solicituds 
        [ResponseType(typeof(Solicitud))]
        public async Task<IHttpActionResult> PostSolicitud(string datos)
        {
            string[] a = datos.Split(',');
            Solicitud solicitud = new Solicitud();
          //  solicitud.Id = id;
            solicitud.Correo = a[0];
            solicitud.Coordinador =  Convert.ToDecimal(a[1]);
            solicitud.IdIntegrante = Convert.ToDecimal(a[2]);
            solicitud.NombreIntegrante = a[3];
            solicitud.DescripcionPorqueQuiereIngresar = a[4];
            solicitud.IdSemolleroInvestigacion = Convert.ToDecimal(a[5]);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Solicitud.Add(solicitud);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SolicitudExists(solicitud.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitud.Id }, solicitud);
        }

        // DELETE: api/Solicituds/5
        [ResponseType(typeof(Solicitud))]
        public async Task<IHttpActionResult> DeleteSolicitud(decimal id)
        {
            Solicitud solicitud = await db.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            db.Solicitud.Remove(solicitud);
            await db.SaveChangesAsync();

            return Ok(solicitud);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SolicitudExists(decimal id)
        {
            return db.Solicitud.Count(e => e.Id == id) > 0;
        }
    }
}