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
using CarPool.Database;

namespace WebApplication2.Controllers
{
    public class OfferController : ApiController
    {
        private CarPoolEntities db = new CarPoolEntities();

        // GET: api/Students
        public IQueryable<OfferTable> GetStudents()
        {
            return db.Offers;
        }

        // GET: api/Students/5
        [ResponseType(typeof(OfferTable))]
        public async Task<IHttpActionResult> GetStudent(string id)
        {
            OfferTable Offer = await db.Offers.FindAsync(id);
            if (Offer == null)
            {
                return NotFound();
            }

            return Ok(Offer);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(string id, OfferTable offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offer.ID)
            {
                return BadRequest();
            }

            db.Entry(offer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(OfferTable))]
        public async Task<IHttpActionResult> PostStudent(OfferTable offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Offers.Add(offer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfferExists(offer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offer.ID }, offer);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(OfferTable))]
        public async Task<IHttpActionResult> DeleteStudent(string id)
        {
            OfferTable offer = await db.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            db.Offers.Remove(offer);
            await db.SaveChangesAsync();

            return Ok(offer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfferExists(string id)
        {
            return db.Offers.Count(e => e.ID == id) > 0;
        }
    }
}

