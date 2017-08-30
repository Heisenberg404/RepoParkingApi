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
    public class PricesController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();

        // GET: api/Prices
        public IQueryable<Price> GetPrice()
        {
            return db.Price;
        }

        // GET: api/Prices/5
        [ResponseType(typeof(Price))]
        public IHttpActionResult GetPrice(int id)
        {
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return NotFound();
            }

            return Ok(price);
        }

        // PUT: api/Prices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrice(int id, Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != price.id)
            {
                return BadRequest();
            }

            db.Entry(price).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/Prices
        [ResponseType(typeof(Price))]
        public IHttpActionResult PostPrice(Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Price.Add(price);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = price.id }, price);
        }

        // DELETE: api/Prices/5
        [ResponseType(typeof(Price))]
        public IHttpActionResult DeletePrice(int id)
        {
            Price price = db.Price.Find(id);
            if (price == null)
            {
                return NotFound();
            }

            db.Price.Remove(price);
            db.SaveChanges();

            return Ok(price);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceExists(int id)
        {
            return db.Price.Count(e => e.id == id) > 0;
        }
    }
}