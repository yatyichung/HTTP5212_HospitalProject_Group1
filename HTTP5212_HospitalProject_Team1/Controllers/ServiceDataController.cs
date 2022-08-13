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
    public class ServiceDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// returns all services
        /// </summary>
        /// <example>
        // GET: api/ServiceData/ListServices
        /// </example>
        [HttpGet]
        public IEnumerable<ServiceDto> ListServices()
        {
            List<Service> Services = db.Services.ToList();
            List<ServiceDto> ServiceDtos = new List<ServiceDto>();

            Services.ForEach(s => ServiceDtos.Add(new ServiceDto()
            {
                serv_id = s.serv_id,
                serv_name = s.serv_name,
                serv_desc = s.serv_desc,
                DepartmentId = s.Department.dept_id,
                DepartmentName = s.Department.dept_name

            }));
            return ServiceDtos;
        }

        [ResponseType(typeof(ServiceDto))]
        [HttpGet]
        public IHttpActionResult ListServicesForDept(int id)
        {
            List<Service> Services = db.Services.Where(a => a.serv_id == id).ToList();
            List<ServiceDto> ServiceDtos = new List<ServiceDto>();

            Services.ForEach(s => ServiceDtos.Add(new ServiceDto()
            {
                serv_id = s.serv_id,
                serv_name = s.serv_name,
                DepartmentId = s.Department.dept_id,
                DepartmentName = s.Department.dept_name,
            }));

            return Ok(ServiceDtos);
        }
        /// <summary>
        /// FIND- returns all services
        /// </summary>
        /// <example>
        // GET: api/ServiceData/FindService/5
        /// </example>
        [ResponseType(typeof(Service))]
        [HttpGet]
        public IHttpActionResult FindService(int id)
        {
            Service service = db.Services.Find(id);
            ServiceDto ServiceDto = new ServiceDto()
            {
                serv_id = service.serv_id,
                serv_name = service.serv_name,
                serv_desc = service.serv_desc,
                DepartmentId = service.Department.dept_id,
                DepartmentName = service.Department.dept_name
            };

            if (service == null)
            {
                return NotFound();
            }

            return Ok(ServiceDto);
        }
        /// <summary>
        /// updates selected service
        /// </summary>
        /// <example>
        // POST: api/ServiceData/UpdateService/5
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.serv_id)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
        /// <summary>
        /// add new service
        /// </summary>
        /// <example>
        // POST: api/ServiceData/AddService
        /// </example>
        [ResponseType(typeof(Service))]
        [HttpPost]
        public IHttpActionResult AddService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Services.Add(service);
            db.SaveChanges();
            
            return CreatedAtRoute("DefaultApi", new { id = service.serv_id }, service);
        }
        /// <summary>
        /// delete selected service
        /// </summary>
        /// <example>
        // POST: api/ServiceData/DeleteService/5
        /// </example>
        [ResponseType(typeof(Service))]
        [HttpPost]
        public IHttpActionResult DeleteService(int id)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            db.Services.Remove(service);
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
        private bool ServiceExists(int id)
        {
            return db.Services.Count(e => e.serv_id == id) > 0;
        }
    }
}