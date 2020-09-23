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
    public class CantantesController : ApiController
    {
        private ExamenTechnologyEntities db = new ExamenTechnologyEntities();

        // GET: api/Cantantes
        public List<Cantante> GetCantante()
        {
            var cantantes = db.Cantante.ToList();
            return cantantes;
        }

        // GET: api/Cantantes/5
        [ResponseType(typeof(Cantante))]
        public IHttpActionResult GetCantante(int id)
        {
            Cantante cantante = db.Cantante.Find(id);
            if (cantante == null)
            {
                return NotFound();
            }

            return Ok(cantante);
        }

        public IEnumerable<Cantante> getCantantesPorNombre (string nombreCantante)
        {
            var cantante = db.Cantante.ToList().Where(x => x.name == nombreCantante);
            return cantante;
        }

        // PUT: api/Cantantes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCantante(int id, Cantante cantante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cantante.id)
            {
                return BadRequest();
            }

            db.Entry(cantante).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CantanteExists(id))
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

        // POST: api/Cantantes
        [ResponseType(typeof(Cantante))]
        public IHttpActionResult PostCantante(Cantante cantante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cantante.Add(cantante);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cantante.id }, cantante);
        }

        // DELETE: api/Cantantes/5
        [ResponseType(typeof(Cantante))]
        public IHttpActionResult DeleteCantante(int id)
        {
            Cantante cantante = db.Cantante.Find(id);
            if (cantante == null)
            {
                return NotFound();
            }

            db.Cantante.Remove(cantante);
            db.SaveChanges();

            return Ok(cantante);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CantanteExists(int id)
        {
            return db.Cantante.Count(e => e.id == id) > 0;
        }
    }
}