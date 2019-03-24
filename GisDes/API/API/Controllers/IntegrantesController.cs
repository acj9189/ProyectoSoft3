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
    public class IntegrantesController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/Integrantes
        public IQueryable<Integrante> GetIntegrante()
        {
            return db.Integrante;
        }

        // GET: api/Integrantes/5
        [ResponseType(typeof(Integrante))]
        public async Task<IHttpActionResult> GetIntegrante(decimal id)
        {
            Integrante integrante = await db.Integrante.FindAsync(id);
            if (integrante == null)
            {
                return NotFound();
            }

            return Ok(integrante);
        }

        // PUT: api/Integrantes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIntegrante(decimal id, Integrante integrante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != integrante.Id)
            {
                return BadRequest();
            }

            db.Entry(integrante).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegranteExists(id))
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

        // POST: api/Integrantes
        [ResponseType(typeof(Integrante))]
        public async Task<IHttpActionResult> PostIntegrante(Integrante integrante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Integrante.Add(integrante);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = integrante.Id }, integrante);
        }

        // DELETE: api/Integrantes/5
        [ResponseType(typeof(Integrante))]
        public async Task<IHttpActionResult> DeleteIntegrante(decimal id)
        {
            Integrante integrante = await db.Integrante.FindAsync(id);
            if (integrante == null)
            {
                return NotFound();
            }

            db.Integrante.Remove(integrante);
            await db.SaveChangesAsync();

            return Ok(integrante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IntegranteExists(decimal id)
        {
            return db.Integrante.Count(e => e.Id == id) > 0;
        }
    }
}