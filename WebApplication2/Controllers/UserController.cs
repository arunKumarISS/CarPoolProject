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
    public class UserController : ApiController
    {
        
            private CarPoolEntities db = new CarPoolEntities();

            // GET: api/Students
            public IQueryable<UserTable> GetStudents()
            {
                return db.Users;
            }

            // GET: api/Students/5
            [ResponseType(typeof(UserTable))]
            public async Task<IHttpActionResult> GetStudent(string id)
            {
                UserTable User = await db.Users.FindAsync(id);
                if (User == null)
                {
                    return NotFound();
                }

                return Ok(User);
            }

            // PUT: api/Students/5
            [ResponseType(typeof(void))]
            public async Task<IHttpActionResult> PutStudent(string id, UserTable user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != user.ID)
                {
                    return BadRequest();
                }

                db.Entry(user).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
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
            [ResponseType(typeof(UserTable))]
            public async Task<IHttpActionResult> PostStudent(UserTable user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Users.Add(user);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (UserExists(user.ID))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
            }

            // DELETE: api/Students/5
            [ResponseType(typeof(UserTable))]
            public async Task<IHttpActionResult> DeleteStudent(string id)
            {
                UserTable user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();

                return Ok(user);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }

            private bool UserExists(string id)
            {
                return db.Users.Count(e => e.ID == id) > 0;
            }
        }
    
}
