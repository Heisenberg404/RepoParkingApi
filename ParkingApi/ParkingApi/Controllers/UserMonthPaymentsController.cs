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
    public class UserMonthPaymentsController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();

        // GET: api/UserMonthPayments
        public IQueryable<UserMonthPayment> GetUserMonthPayment()
        {
            return db.UserMonthPayment;
        }

        // GET: api/UserMonthPayments/5
        [ResponseType(typeof(UserMonthPayment))]
        public IHttpActionResult GetUserMonthPayment(int id)
        {
            UserMonthPayment userMonthPayment = db.UserMonthPayment.Find(id);
            if (userMonthPayment == null)
            {
                return NotFound();
            }

            return Ok(userMonthPayment);
        }

        // PUT: api/UserMonthPayments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserMonthPayment(int id, UserMonthPayment userMonthPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userMonthPayment.id)
            {
                return BadRequest();
            }

            db.Entry(userMonthPayment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMonthPaymentExists(id))
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

        // POST: api/UserMonthPayments
        [ResponseType(typeof(UserMonthPayment))]
        public IHttpActionResult PostUserMonthPayment(UserMonthPayment userMonthPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserMonthPayment.Add(userMonthPayment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userMonthPayment.id }, userMonthPayment);
        }

        // DELETE: api/UserMonthPayments/5
        [ResponseType(typeof(UserMonthPayment))]
        public IHttpActionResult DeleteUserMonthPayment(int id)
        {
            UserMonthPayment userMonthPayment = db.UserMonthPayment.Find(id);
            if (userMonthPayment == null)
            {
                return NotFound();
            }

            db.UserMonthPayment.Remove(userMonthPayment);
            db.SaveChanges();

            return Ok(userMonthPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserMonthPaymentExists(int id)
        {
            return db.UserMonthPayment.Count(e => e.id == id) > 0;
        }
    }
}