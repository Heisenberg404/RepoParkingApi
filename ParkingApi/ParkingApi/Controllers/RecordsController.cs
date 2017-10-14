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

        [ResponseType(typeof(String))]
        public IHttpActionResult PostRecord(RecordRequest recordRequest)
        {
            Record record = new Record();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                record = recordModel.InsertRecord(recordRequest);
                return Ok(record);

            }

        }

        [ResponseType(typeof(Invoice))]
        public IHttpActionResult PutRecord(RecordRequest recordRequest)
        {
            Record record = new Record();
            Invoice invoice = new Invoice();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                invoice = recordModel.QuitRecord(recordRequest);
                return Json(invoice);                
            }

        }

        // GET: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecord(int id)
        {
            Record record = db.Record.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }
        
         
        // DELETE: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult DeleteRecord(int id)
        {
            Record record = db.Record.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            db.Record.Remove(record);
            db.SaveChanges();

            return Ok(record);
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