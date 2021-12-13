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
using WebApi02;

namespace WebApi02.Controllers
{
    public class QuiresController : ApiController
    {
        private dbModel db = new dbModel();

        // GET: api/Quires
        public IQueryable<Quire> GetQuire()
        {
            return db.Quire;
        }

        // GET: api/Quires/5
        [ResponseType(typeof(Quire))]
        public IHttpActionResult GetQuire(int id)
        {
            Quire quire = db.Quire.Find(id);
            if (quire == null)
            {
                return NotFound();
            }

            return Ok(quire);
        }

        // PUT: api/Quires/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuire(Quire quire)
        {
            int id = quire.IdQuire;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quire.IdQuire)
            {
                return BadRequest();
            }

            db.Entry(quire).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuireExists(id))
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

        // POST: api/Quires
        [ResponseType(typeof(Quire))]
        public IHttpActionResult PostQuire(Quire quire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quire.Add(quire);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = quire.IdQuire }, quire);
        }

        // DELETE: api/Quires/5
        [ResponseType(typeof(Quire))]
        public IHttpActionResult DeleteQuire(int id)
        {
            Quire quire = db.Quire.Find(id);
            if (quire == null)
            {
                return NotFound();
            }

            db.Quire.Remove(quire);
            db.SaveChanges();

            return Ok(quire);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuireExists(int id)
        {
            return db.Quire.Count(e => e.IdQuire == id) > 0;
        }
    }
}