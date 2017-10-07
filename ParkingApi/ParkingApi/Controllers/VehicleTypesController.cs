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

namespace ParkingApi.Controllers
{
    public class VehicleTypesController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        VehicleTypesModel vehicleTypeModelo = new VehicleTypesModel();

        // GET: api/VehicleTypes
        public IHttpActionResult GetVehicleType()
        {
            return Json(vehicleTypeModelo.SelectAll());
        }

        // GET: api/VehicleTypes/5
        [ResponseType(typeof(VehicleType))]
        public IHttpActionResult GetVehicleType(int id)
        {

            VehicleType vehicleType = vehicleTypeModelo.GetByIdVehicleType(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return Ok(vehicleType);
        }


        private bool VehicleTypeExists(int id)
        {
            return db.VehicleType.Count(e => e.id == id) > 0;
        }
    }
}