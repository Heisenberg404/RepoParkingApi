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
using AttributeRouting.Web.Mvc;

namespace ParkingApi.Controllers
{
    public class PricesController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        PricesModel priceModel = new PricesModel();
        public String mensaje = " ";

        //Servicio para obtener todos los precios registrados.
        // GET: api/Prices
        public IQueryable<Price> GetPrice()
        {
            return priceModel.SelectAll();
        }

        //Servicio para insertar un registro de precio
        // POST: api/Prices
        [ResponseType(typeof(String))]
        public IHttpActionResult PostPrice(PriceRequest priceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mensaje = priceModel.InsertPrice(priceRequest);

            if (mensaje == "OK")
            {
                return Ok("Insert Ok");
            }
            else
            {
                
                return Ok(mensaje);
            }
            
        }

        //Servicio para obtener precio segun el tipo de vehiculo
        //GET api/Prices/2
        [ResponseType(typeof(Price))]
        public IHttpActionResult GetPrice(int IdVehicleType)
        {
            Price price = priceModel.GetPriceByVehicleType(IdVehicleType);

            if (price == null)
            {
                return NotFound();
            }

            return Ok(price);
        }

        //Servicio para modificar un precio
        // PUT: api/Prices/5
        [ResponseType(typeof(String))]
        public IHttpActionResult PutUser(int id, Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != price.id)
            {
                return BadRequest();
            }
            mensaje = priceModel.UpdatePrice(id, price);
            if (mensaje == "OK")
            {
                //return StatusCode(HttpStatusCode.OK);
                /*return new System.Web.Http.Results.ResponseMessageResult(
                    Request.CreateResponse(
                        (HttpStatusCode)205,
                        new HttpError("updated OK")
                    )
                );*/
                return Ok("Update Ok");


            }
            else
            {
                return NotFound();
            }

        }

        //Servicio para eliminar un registro especifico
        // DELETE: api/Prices/5
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteUser(int id)
        {
            Price price = priceModel.GetByIdPrice(id);
            if (price == null)
            {
                return NotFound();
            }
            mensaje = priceModel.RemovePrice(price);
            if (mensaje == "OK")
            {

                return Ok(price);
            }
            return NotFound();
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