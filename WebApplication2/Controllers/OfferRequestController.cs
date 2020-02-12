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
    public class OfferRequestController : ApiController
    {
        private CarPoolEntities db = new CarPoolEntities();

        // GET: api/Students
        public IQueryable<OfferRequestTable> GetStudents()
        {
            return db.OfferRequests;
        }

        // GET: api/Students/5
        [ResponseType(typeof(OfferRequestTable))]
        public async Task<IHttpActionResult> GetStudent(string id)
        {
            OfferRequestTable offerRequest = await db.OfferRequests.FindAsync(id);
            if (offerRequest == null)
            {
                return NotFound();
            }

            return Ok(offerRequest);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(string id, OfferRequestTable offerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offerRequest.ID)
            {
                return BadRequest();
            }

            db.Entry(offerRequest).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferRequestExists(id))
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
        [ResponseType(typeof(OfferRequestTable))]
        public async Task<IHttpActionResult> PostStudent(OfferRequestTable offerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfferRequests.Add(offerRequest);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfferRequestExists(offerRequest.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = offerRequest.ID }, offerRequest);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(OfferRequestTable))]
        public async Task<IHttpActionResult> DeleteStudent(string id)
        {
            OfferRequestTable offerRequest = await db.OfferRequests.FindAsync(id);
            if (offerRequest == null)
            {
                return NotFound();
            }

            db.OfferRequests.Remove(offerRequest);
            await db.SaveChangesAsync();

            return Ok(offerRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfferRequestExists(string id)
        {
            return db.OfferRequests.Count(e => e.ID == id) > 0;
        }
    }
}

