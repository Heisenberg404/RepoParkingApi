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

namespace ParkingApi.Models
{
    public class VehicleTypesModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //PROBAR
        public List<VehicleTypeResponse> SelectAll()
        {
            List<VehicleTypeResponse> lstTypeVehicle = new List<VehicleTypeResponse>();

            IQueryable<VehicleType> g = db.VehicleType;
            List<VehicleType> pa = g.ToList();
            pa.ForEach(x => {
                VehicleTypeResponse obj = new VehicleTypeResponse();
                obj.id = x.id;
                obj.description = x.description;
                lstTypeVehicle.Add(obj);
            });



            return lstTypeVehicle;
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
