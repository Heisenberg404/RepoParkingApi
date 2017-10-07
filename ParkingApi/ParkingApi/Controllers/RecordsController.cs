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
using AttributeRouting.Web.Mvc;
using ParkingApi.Domain;

namespace ParkingApi.Controllers
{
    public class RecordsController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        RecordsModel recordModel = new RecordsModel();

        // GET: api/Records
        public IHttpActionResult GetRecord()
        {
            return Json(recordModel.SelectAll());
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordExists(int id)
        {
            return db.Record.Count(e => e.Id == id) > 0;
        }
    }
}