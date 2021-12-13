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
    public class RolisController : ApiController
    {
        private dbModel db = new dbModel();

        // GET: api/Rolis
        public IQueryable<Roli> GetRoli()
        {
            return db.Roli;
        }

        // GET: api/Rolis/5
        [ResponseType(typeof(Roli))]
        public IHttpActionResult GetRoli(int id)
        {
            Roli roli = db.Roli.Find(id);
            if (roli == null)
            {
                return NotFound();
            }

            return Ok(roli);
        }

        // PUT: api/Rolis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoli(int id, Roli roli)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roli.IdRoli)
            {
                return BadRequest();
            }

            db.Entry(roli).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoliExists(id))
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

        // POST: api/Rolis
        [ResponseType(typeof(Roli))]
        public IHttpActionResult PostRoli(Roli roli)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roli.Add(roli);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = roli.IdRoli }, roli);
        }

        // DELETE: api/Rolis/5
        [ResponseType(typeof(Roli))]
        public IHttpActionResult DeleteRoli(int id)
        {
            Roli roli = db.Roli.Find(id);
            if (roli == null)
            {
                return NotFound();
            }

            db.Roli.Remove(roli);
            db.SaveChanges();

            return Ok(roli);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoliExists(int id)
        {
            return db.Roli.Count(e => e.IdRoli == id) > 0;
        }
    }
}