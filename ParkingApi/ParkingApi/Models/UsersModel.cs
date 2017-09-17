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
    public class UsersModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje= "OK";

        public String UpdateUser(int id, User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {   
                    mensaje =  "NO EXISTE EN LA TABLA";
                }
                
            }

            return mensaje;
        }
        
        public User GetByIdUser(int id)
        {
            User user = db.User.Find(id);
            return user;
        }

        public User GetByUsernameUser(String username)
        {
            User myUser = db.User.Single(user => user.username == username);
            return myUser;
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}
