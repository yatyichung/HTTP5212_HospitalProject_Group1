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

        // GET: api/PerscriptionData/ListPerscription
        [HttpGet]
        public IEnumerable<PerscriptionDto> ListPerscription()
        {
            List<Perscription> Perscriptions = db.Perscriptions.ToList();
            List<PerscriptionDto> PerscriptionDtos = new List<PerscriptionDto>();

            Perscriptions.ForEach(p => PerscriptionDtos.Add(new PerscriptionDto()
            {
                PrescriptionId = p.PrescriptionId,
                DateOfPrescription = p.DateOfPrescription,
                Prescription = p.Prescription,
                Dosage = p.Dosage,
                PatientID = p.Patient.PatientID,
                FirstName = p.Patient.FirstName,
                LastName = p.Patient.LastName,
                EmployeeID = p.Employee.EmployeeID,
                EmployeeFirstName = p.Employee.EmployeeFirstName,
                EmployeeLastName = p.Employee.EmployeeLastName
            }));

                return PerscriptionDtos;
        }


        // GET: api/PerscriptionData/FindPerscription/5
        [ResponseType(typeof(Perscription))]
        [HttpGet]
        public IHttpActionResult FindPerscriptionFindPerscription(int id)
        {
            Perscription Perscription = db.Perscriptions.Find(id);
            PerscriptionDto PerscriptionDto = new PerscriptionDto()
            {
                PrescriptionId = Perscription.PrescriptionId,
                DateOfPrescription = Perscription.DateOfPrescription,
                Prescription = Perscription.Prescription,
                Dosage = Perscription.Dosage,
                PatientID = Perscription.Patient.PatientID,
                FirstName = Perscription.Patient.FirstName,
                LastName = Perscription.Patient.LastName,
                EmployeeID = Perscription.Employee.EmployeeID,
                EmployeeFirstName = Perscription.Employee.EmployeeFirstName,
                EmployeeLastName = Perscription.Employee.EmployeeLastName
            };

            if (Perscription == null)
            {
                return NotFound();
            }

            return Ok(PerscriptionDto);
        }

        // Post: api/PerscriptionData/UpdatePerscription/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePerscription(int id, Perscription perscription)
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

        // POST: api/PerscriptionData/AddPerscription
        [ResponseType(typeof(Perscription))]
        [HttpPost]
        public IHttpActionResult AddPerscription(Perscription perscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perscriptions.Add(perscription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = perscription.PrescriptionId }, perscription);
        }

        // DELETE: api/PerscriptionData/DeletePerscription/5
        [ResponseType(typeof(Perscription))]
        [HttpPost]
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