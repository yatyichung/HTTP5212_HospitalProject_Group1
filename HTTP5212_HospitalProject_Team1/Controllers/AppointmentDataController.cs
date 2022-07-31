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
using System.Diagnostics;

namespace HTTP5212_HospitalProject_Team1.Controllers
{
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentData/ListAppointments
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointments()
        {
            List<Appointment> Appointments = db.Appointments.ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentId = a.AppointmentId,
                TypeOfAppointment = a.TypeOfAppointment,
                AppointmentTime = a.AppointmentTime,
                PatientID = a.Patient.PatientID,
                FirstName = a.Patient.FirstName,
                LastName = a.Patient.LastName,
                EmployeeID = a.Employee.EmployeeId,
                EmployeeFirstName = a.Employee.EmployeeFirstName,
                EmployeeLastName = a.Employee.EmployeeLastName,

            }));
            return AppointmentDtos;
        }

        // GET: api/AppointmentData/FindAppointment/5
        [ResponseType(typeof(Appointment))]
        [HttpGet]
        public IHttpActionResult FindAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            AppointmentDto AppointmentDto = new AppointmentDto()
            {
                AppointmentId = appointment.AppointmentId,
                TypeOfAppointment = appointment.TypeOfAppointment,
                AppointmentTime = appointment.AppointmentTime,
                PatientID = appointment.Patient.PatientID,
                FirstName = appointment.Patient.FirstName,
                LastName = appointment.Patient.LastName,
                EmployeeID = appointment.Employee.EmployeeId,
                EmployeeFirstName = appointment.Employee.EmployeeFirstName,
                EmployeeLastName = appointment.Employee.EmployeeLastName,
            };
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(AppointmentDto);
        }

        // POST: api/AppointmentData/UpdateAppointment
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id, Appointment appointment)
        {
            Debug.WriteLine("I have reached the update Appointment method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid!");
                return BadRequest(ModelState);
            }

            if (id != appointment.AppointmentId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("Get parameter" + id);
                Debug.WriteLine("Post parameter" + appointment.AppointmentId);
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("none of the conditions triggered");

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AppointmentData/AddAppointment
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.AppointmentId }, appointment);
        }

        // DELETE: api/AppointmentData/DeleteAppointment/5
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AppointmentId == id) > 0;
        }
    }
}