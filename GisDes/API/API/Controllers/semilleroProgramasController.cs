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
    public class semilleroProgramasController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/semilleroProgramas
        public IQueryable<semilleroPrograma> GetsemilleroPrograma()
        {
            return db.semilleroPrograma;
        }

        // GET: api/semilleroProgramas/5
        [ResponseType(typeof(semilleroPrograma))]
        public async Task<IHttpActionResult> GetsemilleroPrograma(decimal id)
        {
            semilleroPrograma semilleroPrograma = await db.semilleroPrograma.FindAsync(id);
            if (semilleroPrograma == null)
            {
                return NotFound();
            }

            return Ok(semilleroPrograma);
        }

        // PUT: api/semilleroProgramas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutsemilleroPrograma(decimal id, semilleroPrograma semilleroPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != semilleroPrograma.Id)
            {
                return BadRequest();
            }

            db.Entry(semilleroPrograma).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!semilleroProgramaExists(id))
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

        // POST: api/semilleroProgramas
        [ResponseType(typeof(semilleroPrograma))]
        public async Task<IHttpActionResult> PostsemilleroPrograma(semilleroPrograma semilleroPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.semilleroPrograma.Add(semilleroPrograma);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (semilleroProgramaExists(semilleroPrograma.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = semilleroPrograma.Id }, semilleroPrograma);
        }

        // DELETE: api/semilleroProgramas/5
        [ResponseType(typeof(semilleroPrograma))]
        public async Task<IHttpActionResult> DeletesemilleroPrograma(decimal id)
        {
            semilleroPrograma semilleroPrograma = await db.semilleroPrograma.FindAsync(id);
            if (semilleroPrograma == null)
            {
                return NotFound();
            }

            db.semilleroPrograma.Remove(semilleroPrograma);
            await db.SaveChangesAsync();

            return Ok(semilleroPrograma);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool semilleroProgramaExists(decimal id)
        {
            return db.semilleroPrograma.Count(e => e.Id == id) > 0;
        }
    }
}