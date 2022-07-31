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
    public class EmployeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EmployeesData/ListEmployee
        [HttpGet]
        public IEnumerable<EmployeeDto> ListEmployee()
        {
            List<Employee> Employees = db.Employees.ToList();
            List<EmployeeDto> EmployeeDtos = new List<EmployeeDto>();
            Employees.ForEach(a => EmployeeDtos.Add(new EmployeeDto()
            {
                EmployeeId = a.EmployeeId,
                EmployeeFirstName = a.EmployeeFirstName,
                EmployeeLastName = a.EmployeeLastName,
                EmployeeRole = a.EmployeeRole,
                EmployeeJoinDate = a.EmployeeJoinDate
            }));
            return EmployeeDtos;
        }

        // GET: api/EmployeesData/FindEmployee/5
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult FindEmployee(int id)
        {
            Employee Employee = db.Employees.Find(id);
            EmployeeDto EmployeeDto= new EmployeeDto()
            {
                EmployeeId = Employee.EmployeeId,
                EmployeeFirstName = Employee.EmployeeFirstName,
                EmployeeLastName = Employee.EmployeeLastName,
                EmployeeRole = Employee.EmployeeRole,
                EmployeeJoinDate = Employee.EmployeeJoinDate
            };
            if (Employee == null)
            {
                return NotFound();
            }

            return Ok(EmployeeDto);
        }

        // POST: api/EmployeesData/UpdateEmployee/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateEmployee(int id, Employee employee)
        {
            Debug.WriteLine("I have reached the update Employee method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid!");
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter " + id);
                Debug.WriteLine("POST parameter " + employee.EmployeeId);
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    Debug.WriteLine("Employee not found");
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

        // POST: api/EmployeesData/AddEmployee
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, employee);
        }

        // POST: api/EmployeesData/DeleteEmployee/5
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}