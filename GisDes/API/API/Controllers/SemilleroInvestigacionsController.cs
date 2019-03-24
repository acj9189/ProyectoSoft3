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
    public class SemilleroInvestigacionsController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/SemilleroInvestigacions
        public IQueryable<SemilleroInvestigacion> GetSemilleroInvestigacion()
        {
            return db.SemilleroInvestigacion;
        }

        // GET: api/SemilleroInvestigacions/5
        [ResponseType(typeof(SemilleroInvestigacion))]
        public async Task<IHttpActionResult> GetSemilleroInvestigacion(decimal id)
        {
            SemilleroInvestigacion semilleroInvestigacion = await db.SemilleroInvestigacion.FindAsync(id);
            if (semilleroInvestigacion == null)
            {
                return NotFound();
            }

            return Ok(semilleroInvestigacion);
        }

        // PUT: api/SemilleroInvestigacions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSemilleroInvestigacion(decimal id, SemilleroInvestigacion semilleroInvestigacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != semilleroInvestigacion.Id)
            {
                return BadRequest();
            }

            db.Entry(semilleroInvestigacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemilleroInvestigacionExists(id))
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

        // POST: api/SemilleroInvestigacions
        [ResponseType(typeof(SemilleroInvestigacion))]
        public async Task<IHttpActionResult> PostSemilleroInvestigacion(SemilleroInvestigacion semilleroInvestigacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SemilleroInvestigacion.Add(semilleroInvestigacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = semilleroInvestigacion.Id }, semilleroInvestigacion);
        }

        // DELETE: api/SemilleroInvestigacions/5
        [ResponseType(typeof(SemilleroInvestigacion))]
        public async Task<IHttpActionResult> DeleteSemilleroInvestigacion(decimal id)
        {
            SemilleroInvestigacion semilleroInvestigacion = await db.SemilleroInvestigacion.FindAsync(id);
            if (semilleroInvestigacion == null)
            {
                return NotFound();
            }

            db.SemilleroInvestigacion.Remove(semilleroInvestigacion);
            await db.SaveChangesAsync();

            return Ok(semilleroInvestigacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SemilleroInvestigacionExists(decimal id)
        {
            return db.SemilleroInvestigacion.Count(e => e.Id == id) > 0;
        }
    }
}