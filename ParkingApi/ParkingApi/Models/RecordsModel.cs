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
    public class RecordsModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //Funcion para obtener todos los usuarios registrados.
        public List<Report> SelectAll()
        {
            List<Report> returnReport = new List<Report>();

            IQueryable<Reporte_Result> reporte = db.Reporte();
            var pa = reporte.ToList();
            pa.ForEach(x =>
            {
                Report obj = new Report();
                obj.id = (int)x.ID;
                obj.license = x.LINCESE;
                obj.vehicleType = x.VEHICLE_TIPE;
                obj.timeEntry = (System.DateTime)x.TIMEENTRY;
                obj.timeOut = (System.DateTime)x.TIMEOUT;
                obj.minuteInParking = (int)x.MINUTES_IN_PARKING;
                obj.minutePrice = (int)x.MINUTE_PRICE;
                obj.totalPrice = (int)x.TOTAL_PRICE;
                obj.cell = x.CELL;
                returnReport.Add(obj);
            });

            return returnReport;
        }

    }
}
