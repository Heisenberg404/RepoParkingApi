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
using ParkingApi;

namespace ParkingApi.Controllers
{
    public class FloorsController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();

        // GET: api/Floors
        public IQueryable<Floor> GetFloor()
        {
            return db.Floor;
        }

        // GET: api/Floors/5
        [ResponseType(typeof(Floor))]
        public IHttpActionResult GetFloor(int id)
        {
            Floor floor = db.Floor.Find(id);
            if (floor == null)
            {
                return NotFound();
            }

            return Ok(floor);
        }

        // PUT: api/Floors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFloor(int id, Floor floor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != floor.Id)
            {
                return BadRequest();
            }

            db.Entry(floor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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

        // POST: api/Floors
        [ResponseType(typeof(Floor))]
        public IHttpActionResult PostFloor(Floor floor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Floor.Add(floor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = floor.Id }, floor);
        }

        // DELETE: api/Floors/5
        [ResponseType(typeof(Floor))]
        public IHttpActionResult DeleteFloor(int id)
        {
            Floor floor = db.Floor.Find(id);
            if (floor == null)
            {
                return NotFound();
            }

            db.Floor.Remove(floor);
            db.SaveChanges();

            return Ok(floor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FloorExists(int id)
        {
            return db.Floor.Count(e => e.Id == id) > 0;
        }
    }
}