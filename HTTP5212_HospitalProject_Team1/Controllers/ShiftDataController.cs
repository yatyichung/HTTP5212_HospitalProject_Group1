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
    public class ShiftDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ShiftData/ListShifts
        [HttpGet]
        public IEnumerable<ShiftDto> ListShifts()
        {
            List<Shift> Shifts = db.Shifts.ToList();
            List<ShiftDto> ShiftDtos = new List<ShiftDto>();

            Shifts.ForEach(s => ShiftDtos.Add(new ShiftDto()
            {
                ShiftID = s.ShiftID,
                ShiftTime = s.ShiftTime,
                ShiftSun = s.ShiftSun,
                ShiftMon = s.ShiftMon,
                ShiftTues   = s.ShiftTues,
                ShiftWed    = s.ShiftWed, 
                ShiftThurs = s.ShiftThurs,
                ShiftFri = s.ShiftFri,
                ShiftSat = s.ShiftSat,
                EmployeeID = s.Employee.EmployeeID,
                EmployeeFirstName = s.Employee.EmployeeFirstName,
                EmployeeLastName = s.Employee.EmployeeLastName
            })); 

            return ShiftDtos;
        }

        // GET: api/ShiftData/FindShift/5
        [ResponseType(typeof(Shift))]
        [HttpGet]
        public IHttpActionResult FindShift(int id)
        {
            Shift shift = db.Shifts.Find(id);
            ShiftDto ShiftDto = new ShiftDto()
            {
                ShiftID = shift.ShiftID,
                ShiftTime = shift.ShiftTime,
                ShiftSun = shift.ShiftSun,
                ShiftMon = shift.ShiftMon,
                ShiftTues = shift.ShiftTues,
                ShiftWed = shift.ShiftWed,
                ShiftThurs = shift.ShiftThurs,
                ShiftFri = shift.ShiftFri,
                ShiftSat = shift.ShiftSat,
                EmployeeID = shift.Employee.EmployeeID,
                EmployeeFirstName = shift.Employee.EmployeeFirstName,
                EmployeeLastName = shift.Employee.EmployeeLastName
            };

            if (shift == null)
            {
                return NotFound();
            }

            return Ok(ShiftDto);
        }

        // POST: api/ShiftData/UpdateShift/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateShift(int id, Shift shift)
        {
            Debug.WriteLine("I have reached the update shift method!");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid!");
                return BadRequest(ModelState);
            }

            if (id != shift.ShiftID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter "+id);
                Debug.WriteLine("POST parameter " + shift.ShiftID);
                return BadRequest();
            }

            db.Entry(shift).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftExists(id))
                {
                    Debug.WriteLine("Shift not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShiftData/AddShift
        [ResponseType(typeof(Shift))]
        [HttpPost]
        public IHttpActionResult AddShift(Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shifts.Add(shift);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shift.ShiftID }, shift);
        }

        // POST: api/ShiftData/DeleteShift/5
        [ResponseType(typeof(Shift))]
        [HttpPost]
        public IHttpActionResult DeleteShift(int id)
        {
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return NotFound();
            }

            db.Shifts.Remove(shift);
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

        private bool ShiftExists(int id)
        {
            return db.Shifts.Count(e => e.ShiftID == id) > 0;
        }
    }
}