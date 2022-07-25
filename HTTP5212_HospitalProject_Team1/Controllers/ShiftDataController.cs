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
    public class ShiftDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ShiftData/ListShifts
        [HttpGet]
        public IEnumerable<ShiftDto> ListShifts()
        {
            List<Shift> Shifts = db.Shifts.ToList();
            List<ShiftDto> ShiftDtos = new List<ShiftDto>();

            Shifts.ForEach(a => ShiftDtos.Add(new ShiftDto()
            {
                ShiftID = a.ShiftID,
                ShiftTime = a.ShiftTime,
                ShiftSun = a.ShiftSun,
                ShiftMon = a.ShiftMon,
                ShiftTues   = a.ShiftTues,
                ShiftWed    = a.ShiftWed, 
                ShiftThurs = a.ShiftThurs,
                ShiftFri = a.ShiftFri,
                ShiftSat = a.ShiftSat,
                EmployeeID = a.Employee.EmployeeID,
                EmployeeLastName = a.Employee.EmployeeLastName
            })); 

            return ShiftDtos;
        }

        // GET: api/ShiftData/FindShift/5
        [ResponseType(typeof(Shift))]
        [HttpGet]
        public IHttpActionResult FindShift(int id)
        {
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        // POST: api/ShiftData/UpdateShift/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateShift(int id, Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shift.ShiftID)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            return Ok(shift);
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