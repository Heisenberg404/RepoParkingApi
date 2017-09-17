using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ParkingApi;
using ParkingApi.Domain;
using System.Data.Entity.Validation;

namespace ParkingApi.Models
{
    public class PricesModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //Funcion para obtener todos los precios registrados.
        public IQueryable<Price> SelectAll()
        {

            return db.Price;
        }
        //Funcion para insertar un nuevo registro
        public String InsertPrice(PriceRequest priceRequest)
        {
            try
            {
                Price priceResponse = GetPriceByVehicleType(priceRequest.idVehicleType);
                if (priceResponse == null)
                {
                    Price price = new Price();
                    price.idVehicleType = priceRequest.idVehicleType;
                    price.valueMinute = priceRequest.valueMinute;
                    price.valueMonth = priceRequest.valueMonth;
                    db.Price.Add(price);
                    db.SaveChanges();
                    mensaje = "OK";
                }
                else
                {
                    mensaje = "PRICE_EXIST_FOR_THIS_VEHICLE_TYPE";

                }
            
             }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al crear un precio" + e;
            }

            return mensaje;
        }

        //funcion para obtener el precio segun el tipo de vehiculo
        public Price GetPriceByVehicleType(int IdVehicleType)
        {
            Price myPrice = db.Price.SingleOrDefault(Price => Price.idVehicleType == IdVehicleType);
            return myPrice;
        }

        //Funcion para modificar un precio
        public String UpdatePrice(int id, Price Price)
        {
            try
            {
                db.Entry(Price).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
                {
                    mensaje = "NOT_FOUND";
                }

            }

            return mensaje;
        }

        public Price GetByIdPrice(int id)
        {
            Price Price = db.Price.Find(id);
            return Price;
        }



        public String RemovePrice(Price Price)
        {
            try
            {
                db.Price.Remove(Price);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al eliminar un precio" + e;
            }
            return mensaje;
        }

        private bool PriceExists(int id)
        {
            return db.Price.Count(e => e.id == id) > 0;
        }
    }
}
