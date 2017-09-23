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
    public class UsersModel
    {
        private ParkingEntities db = new ParkingEntities();
        public String mensaje= "OK";
        
        //Funcion para obtener todos los usuarios registrados.
        public IQueryable<User> SelectAll()
        {
            return db.User;
        }

        public String InsertUser(UserRequest userRequest)
        {
            try
            {
                mensaje = this.GetByUsernameUser(userRequest.username);
                if (mensaje == "NOT_FOUND")
                {
                    User user = new User();
                    user.username = userRequest.username;
                    user.pass = userRequest.pass;
                    db.User.Add(user);
                    db.SaveChanges();
                    mensaje = "OK";

                }
                else
                {
                    mensaje = "USER_EXIST";

                }
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al crear un usuario"+ e;
            }
            return mensaje;
        }

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
                    mensaje =  "NOT_FOUND";
                }
                
            }

            return mensaje;
        }
        
        public User GetByIdUser(int id)
        {
            User user = db.User.Find(id);
            return user;
        }

        public String GetByUsernameUser(String username)
        {
            User myUser =db.User.SingleOrDefault(user => user.username == username);
            if (myUser == null)
            {
                mensaje = "NOT_FOUND";
            }

            return mensaje;
        }

        public String GetByUsernameAndPassUser(UserRequest userRequest)
        {
            IQueryable<User> u = db.User.Where(User => User.username == userRequest.username);
            User user = u.SingleOrDefault(User => User.pass == userRequest.pass);
            if (user == null)
            {
                mensaje = "NOT_FOUND";
            }

            return mensaje;
        }
        public String RemoveUser(User user)
        {
            try
            {
                db.User.Remove(user);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al eliminar un usuario" + e;
            }
            return mensaje;
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.id == id) > 0;
        }
    }
}
