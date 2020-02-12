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
    public class PaymentController : ApiController
    {
        private CarPoolEntities db = new CarPoolEntities();

        // GET: api/Students
        public IQueryable<PaymentTable> GetStudents()
        {
            return db.Payments;
        }

        // GET: api/Students/5
        [ResponseType(typeof(PaymentTable))]
        public async Task<IHttpActionResult> GetStudent(string id)
        {
            PaymentTable payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudent(string id, PaymentTable payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.ID)
            {
                return BadRequest();
            }

            db.Entry(payment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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
        [ResponseType(typeof(PaymentTable))]
        public async Task<IHttpActionResult> PostStudent(PaymentTable payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentExists(payment.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = payment.ID }, payment);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(PaymentTable))]
        public async Task<IHttpActionResult> DeleteStudent(string id)
        {
            PaymentTable payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payment);
            await db.SaveChangesAsync();

            return Ok(payment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentExists(string id)
        {
            return db.Payments.Count(e => e.ID == id) > 0;
        }
    }
}
