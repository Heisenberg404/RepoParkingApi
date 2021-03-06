﻿using System;
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

        public User InsertUser(UserRequest userRequest)
        {
            User userReturn= new User();
            try
            {
                userReturn = this.GetByUsernameUser(userRequest.username);
                if (userReturn == null)
                {
                    User user = new User();
                    user.username = userRequest.username;
                    user.pass = userRequest.pass;
                    db.User.Add(user);
                    db.SaveChanges();
                    mensaje = "OK";
                    userReturn = this.GetByUsernameUser(user.username);
                 }
            }
            catch (DbEntityValidationException e)
            {
                mensaje = "Error al crear un usuario"+ e;
            }
            return userReturn;

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

        public User GetByUsernameUser(String username)
        {
            User myUser =db.User.SingleOrDefault(user => user.username == username);
           

            return myUser;
        }

        public User GetByUsernameAndPassUser(UserRequest userRequest)
        {
            IQueryable<User> u = db.User.Where(User => User.username == userRequest.username);
            User user = u.SingleOrDefault(User => User.pass == userRequest.pass);
            return user;
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
