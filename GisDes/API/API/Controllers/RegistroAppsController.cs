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
    public class RegistroAppsController : ApiController
    {
        private ModelosAPP db = new ModelosAPP();

        // GET: api/RegistroApps
        public IQueryable<RegistroApp> GetRegistroApp()
        {
            return db.RegistroApp;
        }

        // GET: api/RegistroApps/5
        [ResponseType(typeof(RegistroApp))]
        public async Task<IHttpActionResult> GetRegistroApp(decimal id)
        {
            RegistroApp registroApp = await db.RegistroApp.FindAsync(id);
            if (registroApp == null)
            {
                return NotFound();
            }

            return Ok(registroApp);
        }

        // PUT: api/RegistroApps/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRegistroApp(decimal id, RegistroApp registroApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registroApp.Id)
            {
                return BadRequest();
            }

            db.Entry(registroApp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroAppExists(id))
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

        // POST: api/RegistroApps
        [ResponseType(typeof(RegistroApp))]
        public async Task<IHttpActionResult> PostRegistroApp(RegistroApp registroApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RegistroApp.Add(registroApp);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistroAppExists(registroApp.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registroApp.Id }, registroApp);
        }

        // DELETE: api/RegistroApps/5
        [ResponseType(typeof(RegistroApp))]
        public async Task<IHttpActionResult> DeleteRegistroApp(decimal id)
        {
            RegistroApp registroApp = await db.RegistroApp.FindAsync(id);
            if (registroApp == null)
            {
                return NotFound();
            }

            db.RegistroApp.Remove(registroApp);
            await db.SaveChangesAsync();

            return Ok(registroApp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RegistroAppExists(decimal id)
        {
            return db.RegistroApp.Count(e => e.Id == id) > 0;
        }
    }
}