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
using ParkingApi.Models;
using ParkingApi.Domain;

namespace ParkingApi.Controllers
{
    public class ParkCellsController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        ParkCellsModel parkCellsModel = new ParkCellsModel();
         
        // GET: api/ParkCells
        public IHttpActionResult GetParkCells()
        {
            
            return Json(parkCellsModel.SelectAll());
            
        }

        // GET: api/ParkCells/5
        //hola ale
        [ResponseType(typeof(ParkCells))]
        public IHttpActionResult GetParkCells(int id)
        {
            ParkCells parkCells = db.ParkCells.Find(id);
            if (parkCells == null)
            {
                return NotFound();
            }

            return Ok(parkCells);
        }

        // PUT: api/ParkCells/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParkCells(int id, ParkCells parkCells)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parkCells.id)
            {
                return BadRequest();
            }

            db.Entry(parkCells).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkCellsExists(id))
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

        // POST: api/ParkCells
        [ResponseType(typeof(ParkCells))]
        public IHttpActionResult PostParkCells(ParkCells parkCells)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParkCells.Add(parkCells);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = parkCells.id }, parkCells);
        }

        // DELETE: api/ParkCells/5
        [ResponseType(typeof(ParkCells))]
        public IHttpActionResult DeleteParkCells(int id)
        {
            ParkCells parkCells = db.ParkCells.Find(id);
            if (parkCells == null)
            {
                return NotFound();
            }

            db.ParkCells.Remove(parkCells);
            db.SaveChanges();

            return Ok(parkCells);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParkCellsExists(int id)
        {
            return db.ParkCells.Count(e => e.id == id) > 0;
        }
    }
}