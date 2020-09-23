using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ExamenTechnology.Models;

namespace ExamenTechnology.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CancionsController : ApiController
    {
        private ExamenTechnologyEntities db = new ExamenTechnologyEntities();

        // GET: api/Cancions
        public IEnumerable<Cancion> GetCancion()
        {
            return db.Cancion;
        }

        // GET: api/Cancions/5
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult GetCancion(int id)
        {
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return NotFound();
            }

            return Ok(cancion);
        }

       

        // PUT: api/Cancions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCancion(int id, Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cancion.idCancion)
            {
                return BadRequest();
            }

            db.Entry(cancion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionExists(id))
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

        // POST: api/Cancions
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult PostCancion(Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cancion.Add(cancion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cancion.idCancion }, cancion);
        }

        // DELETE: api/Cancions/5
        [ResponseType(typeof(Cancion))]
        public IHttpActionResult DeleteCancion(int id)
        {
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return NotFound();
            }

            db.Cancion.Remove(cancion);
            db.SaveChanges();

            return Ok(cancion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CancionExists(int id)
        {
            return db.Cancion.Count(e => e.idCancion == id) > 0;
        }
    }
}