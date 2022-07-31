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
        public class DepartmentDataController : ApiController
        {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: api/DepartmentData/ListDepartments
            [HttpGet]
            public IEnumerable<DepartmentDto> ListDepartments()
            {
                List<Department> Departments = db.Departments.ToList();
                List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

                Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
                {
                    dept_id = d.dept_id,
                    dept_name = d.dept_name,
                    dept_desc = d.dept_desc
                }));
                return DepartmentDtos;
            }

            // GET: api/DepartmentData/FindDepartment/5
            [ResponseType(typeof(Department))]
            [HttpGet]
            public IHttpActionResult FindDepartment(int id)
            {
                Department department = db.Departments.Find(id);
                DepartmentDto DepartmentDto = new DepartmentDto()
                {
                    dept_id = department.dept_id,
                    dept_name = department.dept_name,
                    dept_desc = department.dept_desc
                };

                if (department == null)
                {
                    return NotFound();
                }

                return Ok(DepartmentDto);
            }

            // POST: api/DepartmentData/UpdateDepartment/5
            [ResponseType(typeof(void))]
            [HttpPost]
            public IHttpActionResult UpdateDepartment(int id, Department department)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != department.dept_id)
                {
                    return BadRequest();
                }

                db.Entry(department).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(id))
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

            // POST: api/DepartmentData/AddDepartment
            [ResponseType(typeof(Department))]
            [HttpPost]
            public IHttpActionResult AddDepartment(Department department)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                db.Departments.Add(department);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = department.dept_id }, department);
            }

            // POST: api/DepartmentData/DeleteDepartment/5
            [ResponseType(typeof(Department))]
            [HttpPost]
            public IHttpActionResult DeleteDepartment(int id)
            {
                Department department = db.Departments.Find(id);
                if (department == null)
                {
                    return NotFound();
                }
                db.Departments.Remove(department);
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
            private bool DepartmentExists(int id)
            {
                return db.Departments.Count(e => e.dept_id == id) > 0;
            }
        }
    }