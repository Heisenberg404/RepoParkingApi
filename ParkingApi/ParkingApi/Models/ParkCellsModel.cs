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
    public class ParkCellsModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje = "OK";

        //Funcion para obtener todos los usuarios registrados.
        public IQueryable<ParkCells> SelectAll()
        {
            return db.ParkCells;
        }

       

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}
