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
    public class RoomDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RoomData/ListRooms
        [HttpGet]
        [ResponseType(typeof(RoomDto))]
        public IEnumerable<RoomDto> ListRooms()
        {
            List<Room> Rooms = db.Rooms.ToList();
            List<RoomDto> RoomDtos = new List<RoomDto>();

            Rooms.ForEach(r => RoomDtos.Add(new RoomDto()
            {
                RoomId = r.RoomId,
                RoomType = r.RoomType,
                RoomNumber = r.RoomNumber,
                Availability = r.Availability,
                PatientID = r.Patient.PatientID,
                FirstName = r.Patient.FirstName,
                LastName = r.Patient.LastName,
            }));

            return RoomDtos;
        }

        // GET: api/RoomData/FindRooms/5
        [ResponseType(typeof(Room))]
        [HttpGet]
        public IHttpActionResult FindRoom(int id)
        {
            Room Room = db.Rooms.Find(id);
            RoomDto RoomDto = new RoomDto()
            {
                RoomId = Room.RoomId,
                RoomType = Room.RoomType,
                RoomNumber = Room.RoomNumber,
                Availability = Room.Availability,
                PatientID = Room.Patient.PatientID,
                FirstName = Room.Patient.FirstName,
                LastName = Room.Patient.LastName,
            };
            if (Room == null)
            {
                return NotFound();
            }

            return Ok(RoomDto);
        }

        // POST: api/RoomData/UpdateRoom/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/RoomData/AddRoom
        [ResponseType(typeof(Room))]
        [HttpPost]
        public IHttpActionResult AddRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // POST: api/RoomData/DeleteRoom/5
        [ResponseType(typeof(Room))]
        [HttpPost]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
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

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomId == id) > 0;
        }
    }
}