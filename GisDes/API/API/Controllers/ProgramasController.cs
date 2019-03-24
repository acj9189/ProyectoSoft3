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
    public class ProgramasController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/Programas
        public IQueryable<Programa> GetPrograma()
        {
            return db.Programa;
        }

        // GET: api/Programas/5
        [ResponseType(typeof(Programa))]
        public async Task<IHttpActionResult> GetPrograma(decimal id)
        {
            Programa programa = await db.Programa.FindAsync(id);
            if (programa == null)
            {
                return NotFound();
            }

            return Ok(programa);
        }

        // PUT: api/Programas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrograma(decimal id, Programa programa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programa.Id)
            {
                return BadRequest();
            }

            db.Entry(programa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramaExists(id))
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

        // POST: api/Programas
        [ResponseType(typeof(Programa))]
        public async Task<IHttpActionResult> PostPrograma(Programa programa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Programa.Add(programa);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgramaExists(programa.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = programa.Id }, programa);
        }

        // DELETE: api/Programas/5
        [ResponseType(typeof(Programa))]
        public async Task<IHttpActionResult> DeletePrograma(decimal id)
        {
            Programa programa = await db.Programa.FindAsync(id);
            if (programa == null)
            {
                return NotFound();
            }

            db.Programa.Remove(programa);
            await db.SaveChangesAsync();

            return Ok(programa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgramaExists(decimal id)
        {
            return db.Programa.Count(e => e.Id == id) > 0;
        }
    }
}