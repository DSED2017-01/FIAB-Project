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
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{

    public class MARINE_SPECIES_APIController : ApiController
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: api/MARINE_SPECIES_API
        [HttpGet]
        //public IQueryable<MARINE_SPECIES> GetMARINE_SPECIES()
        public IEnumerable<MARINE_SPECIES> GetMARINE_SPECIES()
        {
            return db.MARINE_SPECIES;
        }

        // GET: api/MARINE_SPECIES_API/5
        [ResponseType(typeof(MARINE_SPECIES))]
        public async Task<IHttpActionResult> GetMARINE_SPECIES(int id)
        {
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            if (mARINE_SPECIES == null)
            {
                return NotFound();
            }

            return Ok(mARINE_SPECIES);
        }

        // PUT: api/MARINE_SPECIES_API/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMARINE_SPECIES(int id, MARINE_SPECIES mARINE_SPECIES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mARINE_SPECIES.ID_PK)
            {
                return BadRequest();
            }

            db.Entry(mARINE_SPECIES).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MARINE_SPECIESExists(id))
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

        // POST: api/MARINE_SPECIES_API
        [ResponseType(typeof(MARINE_SPECIES))]
        public async Task<IHttpActionResult> PostMARINE_SPECIES(MARINE_SPECIES mARINE_SPECIES)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MARINE_SPECIES.Add(mARINE_SPECIES);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mARINE_SPECIES.ID_PK }, mARINE_SPECIES);
        }

        // DELETE: api/MARINE_SPECIES_API/5
        [ResponseType(typeof(MARINE_SPECIES))]
        public async Task<IHttpActionResult> DeleteMARINE_SPECIES(int id)
        {
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            if (mARINE_SPECIES == null)
            {
                return NotFound();
            }

            db.MARINE_SPECIES.Remove(mARINE_SPECIES);
            await db.SaveChangesAsync();

            return Ok(mARINE_SPECIES);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MARINE_SPECIESExists(int id)
        {
            return db.MARINE_SPECIES.Count(e => e.ID_PK == id) > 0;
        }
    }
}