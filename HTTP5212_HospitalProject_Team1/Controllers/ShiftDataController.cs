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


        /// <summary>
        /// Returns all shifts in the system.
        /// </summary>
        /// <returns>
        /// CONTENT: all shifts in the database, linking the correspond employees
        /// </returns>
        /// <example>
        /// GET: api/ShiftData/ListShifts
        /// </example>
        [HttpGet]
        [ResponseType(typeof(ShiftDto))]
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
                EmployeeID = s.Employee.EmployeeId,
                EmployeeFirstName = s.Employee.EmployeeFirstName,
                EmployeeLastName = s.Employee.EmployeeLastName
            })); 

            return ShiftDtos;
        }

        /// <summary>
        /// Returns all shifts in the system.
        /// </summary>
        /// <returns>
        /// CONTENT: A shift in the system matching up to the shifts ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the shift</param>
        /// <example>
        /// GET: api/ShiftData/FindShift/5
        /// </example>
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
                EmployeeID = shift.Employee.EmployeeId,
                EmployeeFirstName = shift.Employee.EmployeeFirstName,
                EmployeeLastName = shift.Employee.EmployeeLastName
            };

            if (shift == null)
            {
                return NotFound();
            }

            return Ok(ShiftDto);
        }

          /// <summary>
        /// Update a shift information in the system using POST data input
        /// </summary>
        /// <param name="id">Represents the Shift ID primary key</param>
        /// <param name="shift">JSON FORM DATA of a shift</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ShiftData/UpdateShift/5
        /// FORM DATA: Shift JSON Object
        /// </example>
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
                //Debug.WriteLine("ID mismatch");
                //Debug.WriteLine("GET parameter "+id);
                //Debug.WriteLine("POST parameter " + shift.ShiftID);
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
            //Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Add a shift to the system
        /// </summary>
        /// <param name="shift">JSON FORM DATA of a shift</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: shift ID, shift Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ShiftData/AddShift
        /// FORM DATA: Shift JSON Object
        /// </example>
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

        /// <summary>
        /// Deletes a shift from the system by it's ID (will only affect shift table).
        /// </summary>
        /// <param name="id">The primary key of the shift</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ShiftData/DeleteShift/5
        /// FORM DATA: (empty)
        /// </example>
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
