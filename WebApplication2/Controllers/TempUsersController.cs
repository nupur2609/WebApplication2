using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TempUsersController : ApiController
    {
        private iarcadeEntities1 db = new iarcadeEntities1();

        // GET: api/TempUsers
        public IQueryable<TempUser> GetTempUsers()
        {
            return db.TempUsers;
        }

        // GET: api/TempUsers/5
        [ResponseType(typeof(TempUser))]
        public IHttpActionResult GetTempUser(int id)
        {
            TempUser tempUser = db.TempUsers.Find(id);
            if (tempUser == null)
            {
                return NotFound();
            }

            return Ok(tempUser);
        }

        // PUT: api/TempUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTempUser(int id, TempUser tempUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tempUser.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(tempUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempUserExists(id))
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

        // POST: api/TempUsers
        [ResponseType(typeof(TempUser))]
        public IHttpActionResult PostTempUser(TempUser tempUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TempUsers.Add(tempUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tempUser.EmployeeID }, tempUser);
        }

        // DELETE: api/TempUsers/5
        [ResponseType(typeof(TempUser))]
        public IHttpActionResult DeleteTempUser(int id)
        {
            TempUser tempUser = db.TempUsers.Find(id);
            if (tempUser == null)
            {
                return NotFound();
            }

            db.TempUsers.Remove(tempUser);
            db.SaveChanges();

            return Ok(tempUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TempUserExists(int id)
        {
            return db.TempUsers.Count(e => e.EmployeeID == id) > 0;
        }
    }
}