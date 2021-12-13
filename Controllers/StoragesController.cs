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
    public class StoragesController : ApiController
    {
        private dbModel db = new dbModel();

        // GET: api/Storages
        public IQueryable<Storage> GetStorage()
        {
            return db.Storage;
        }

        // GET: api/Storages/5
        [ResponseType(typeof(Storage))]
        public IHttpActionResult GetStorage(int id)
        {
            Storage storage = db.Storage.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Storages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStorage(Storage storage)
        {
            int id = storage.IdMaterial;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storage.IdMaterial)
            {
                return BadRequest();
            }

            db.Entry(storage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExists(id))
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

        // POST: api/Storages
        [ResponseType(typeof(Storage))]
        public IHttpActionResult PostStorage(Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Storage.Add(storage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = storage.IdMaterial }, storage);
        }

        // DELETE: api/Storages/5
        [ResponseType(typeof(Storage))]
        public IHttpActionResult DeleteStorage(int id)
        {
            Storage storage = db.Storage.Find(id);
            if (storage == null)
            {
                return NotFound();
            }

            db.Storage.Remove(storage);
            db.SaveChanges();

            return Ok(storage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StorageExists(int id)
        {
            return db.Storage.Count(e => e.IdMaterial == id) > 0;
        }
    }
}