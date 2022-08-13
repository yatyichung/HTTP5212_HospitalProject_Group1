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
    public class PaySlipDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Returns all payslips in the system.
        /// </summary>
        /// <returns>
        /// CONTENT: all payslips in the database, linking the correspond employees
        /// </returns>
        /// <example>
        /// GET: api/PaySlipData/ListPaySlips
        /// </example>
        [HttpGet]
        public IEnumerable<PaySlipDto> ListPaySlips()
        {
            List<PaySlip> PaySlips = db.PaySlips.ToList();
            List<PaySlipDto> PaySlipDtos = new List<PaySlipDto>();

            PaySlips.ForEach(p => PaySlipDtos.Add(new PaySlipDto()
            {
                PaySlipID = p.PaySlipID,
                PaySlipHoursWorked = p.PaySlipHoursWorked,
                PaySlipSinNum = p.PaySlipSinNum,
                PaySlipHourlyWage = p.PaySlipHourlyWage,
                PaySlipPaymentDate = p.PaySlipPaymentDate,
                EmployeeID = p.Employee.EmployeeId,
                EmployeeFirstName = p.Employee.EmployeeFirstName,
                EmployeeLastName = p.Employee.EmployeeLastName
            }));
            return PaySlipDtos;
        }
        
        /// <summary>
        /// Returns all payslips in the system.
        /// </summary>
        /// <returns>
        /// CONTENT: A payslip in the system matching up to the payslip ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the payslip</param>
        /// <example>
        /// GET: api/PaySlipData/FindPaySlip/5
        /// </example>
        [ResponseType(typeof(PaySlip))]
        [HttpGet]
        public IHttpActionResult FindPaySlip(int id)
        {
            PaySlip paySlip = db.PaySlips.Find(id);
            PaySlipDto PaySlipDto = new PaySlipDto()
            {
                PaySlipID = paySlip.PaySlipID,
                PaySlipHoursWorked = paySlip.PaySlipHoursWorked,
                PaySlipSinNum = paySlip.PaySlipSinNum,
                PaySlipHourlyWage = paySlip.PaySlipHourlyWage,
                PaySlipPaymentDate = paySlip.PaySlipPaymentDate,
                EmployeeID = paySlip.Employee.EmployeeId,
                EmployeeFirstName = paySlip.Employee.EmployeeFirstName,
                EmployeeLastName = paySlip.Employee.EmployeeLastName
            };



            if (paySlip == null)
            {
                return NotFound();
            }

            return Ok(PaySlipDto);
        }

        /// <summary>
        /// Update a payslip information in the system using POST data input
        /// </summary>
        /// <param name="id">Represents the PaySlip ID primary key</param>
        /// <param name="payslip">JSON FORM DATA of a payslip</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/PaySlipData/UpdatePaySlip/5
        /// FORM DATA: PaySlip JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePaySlip(int id, PaySlip paySlip)
        {
            Debug.WriteLine("I have reached the update shift method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid!");
                return BadRequest(ModelState);
            }

            if (id != paySlip.PaySlipID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter " + id);
                Debug.WriteLine("POST parameter " + paySlip.PaySlipID);
                return BadRequest();
            }

            db.Entry(paySlip).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaySlipExists(id))
                {
                    Debug.WriteLine("Payslip not found");
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
        
        /// <summary>
        /// Add a payslip to the system
        /// </summary>
        /// <param name="payslip">JSON FORM DATA of a payslip</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: payslip ID, payslip Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/PaySlipData/AddPaySlip
        /// FORM DATA: PaySlip JSON Object
        /// </example>
        [ResponseType(typeof(PaySlip))]
        [HttpPost]
        public IHttpActionResult AddPaySlip(PaySlip payslip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaySlips.Add(payslip);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payslip.PaySlipID }, payslip);
        }

        /// <summary>
        /// Deletes a payslip from the system by it's ID (will only affect shift table).
        /// </summary>
        /// <param name="id">The primary key of the payslip</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/PaySlipData/DeletePaySlip/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(PaySlip))]
        [HttpPost]
        public IHttpActionResult DeletePaySlip(int id)
        {
            PaySlip paySlip = db.PaySlips.Find(id);
            if (paySlip == null)
            {
                return NotFound();
            }

            db.PaySlips.Remove(paySlip);
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

        private bool PaySlipExists(int id)
        {
            return db.PaySlips.Count(e => e.PaySlipID == id) > 0;
        }
    }
}
