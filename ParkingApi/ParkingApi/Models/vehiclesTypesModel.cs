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

namespace ParkingApi.Models
{
    public class VehicleTypesModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //PROBAR
        public IQueryable<VehicleType> SelectAll()
        {
            return db.VehicleType;
        }

        public VehicleType GetByIdVehicleType(int id)
        {
            VehicleType vehicleType = db.VehicleType.Find(id);
            return vehicleType;
        }

        private bool vehicleTypeExists(int id)
        {
            return db.VehicleType.Count(e => e.id == id) > 0;
        }
    }
}
