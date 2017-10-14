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
using ParkingApi.Domain;
using ParkingApi.Models;

namespace ParkingApi.Controllers
{
    public class UserMonthPaymentsController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        UserMonthPaymentsModel userMonthPaymentsModel = new UserMonthPaymentsModel();

        // GET: api/UserMonthPayments
        public IQueryable<UserMonthPayment> GetUserMonthPayments()
        {
            return db.UserMonthPayments;
        }

        // GET: api/UserMonthPayments/5
        [ResponseType(typeof(UserMonthPaymentsResponse))]
        public IHttpActionResult GetUserMonthPayment(int id)
        {
            UserMonthPaymentsResponse userMonthPaymentsResponse = userMonthPaymentsModel.GetById(id);
            return Json(userMonthPaymentsResponse);
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

            db.UserMonthPayments.Add(userMonthPayment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userMonthPayment.id }, userMonthPayment);
        }

        // DELETE: api/UserMonthPayments/5
        [ResponseType(typeof(UserMonthPayment))]
        public IHttpActionResult DeleteUserMonthPayment(int id)
        {
            UserMonthPayment userMonthPayment = db.UserMonthPayments.Find(id);
            if (userMonthPayment == null)
            {
                return NotFound();
            }

            db.UserMonthPayments.Remove(userMonthPayment);
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
            return db.UserMonthPayments.Count(e => e.id == id) > 0;
        }
    }
}