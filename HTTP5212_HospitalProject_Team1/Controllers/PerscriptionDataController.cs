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
using HTTP5212_HospitalProject_Team1.Models;

namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class PerscriptionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PerscriptionData
        public IQueryable<Perscription> GetPerscriptions()
        {
            return db.Perscriptions;
        }

        // GET: api/PerscriptionData/5
        [ResponseType(typeof(Perscription))]
        public IHttpActionResult GetPerscription(int id)
        {
            Perscription perscription = db.Perscriptions.Find(id);
            if (perscription == null)
            {
                return NotFound();
            }

            return Ok(perscription);
        }

        // PUT: api/PerscriptionData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerscription(int id, Perscription perscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perscription.PrescriptionId)
            {
                return BadRequest();
            }

            db.Entry(perscription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerscriptionExists(id))
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

        // POST: api/PerscriptionData
        [ResponseType(typeof(Perscription))]
        public IHttpActionResult PostPerscription(Perscription perscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perscriptions.Add(perscription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = perscription.PrescriptionId }, perscription);
        }

        // DELETE: api/PerscriptionData/5
        [ResponseType(typeof(Perscription))]
        public IHttpActionResult DeletePerscription(int id)
        {
            Perscription perscription = db.Perscriptions.Find(id);
            if (perscription == null)
            {
                return NotFound();
            }

            db.Perscriptions.Remove(perscription);
            db.SaveChanges();

            return Ok(perscription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerscriptionExists(int id)
        {
            return db.Perscriptions.Count(e => e.PrescriptionId == id) > 0;
        }
    }
}