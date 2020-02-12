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
    public class BookingController : ApiController
    {
        private CarPoolEntities db = new CarPoolEntities();

        // GET: api/Students
        public IQueryable<BookingTable> GetStudents()
        {
            return db.Bookings;
        }

        // GET: api/Students/5
        [ResponseType(typeof(BookingTable))]
        public async Task<IHttpActionResult> GetStudent(string id)
        {
            BookingTable Booking = await db.Bookings.FindAsync(id);
            if (Booking == null)
            {
                return NotFound();
            }

            return Ok(Booking);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(string id, BookingTable booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.ID)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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
        [ResponseType(typeof(BookingTable))]
        public async Task<IHttpActionResult> PostStudent(BookingTable booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bookings.Add(booking);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = booking.ID }, booking);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(BookingTable))]
        public async Task<IHttpActionResult> DeleteStudent(string id)
        {
            BookingTable booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingExists(string id)
        {
            return db.Bookings.Count(e => e.ID == id) > 0;
        }
    }
}
}
