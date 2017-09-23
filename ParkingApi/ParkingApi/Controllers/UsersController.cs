
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
using AttributeRouting.Web.Mvc;
using ParkingApi.Domain;

namespace ParkingApi.Controllers
{
    public class UsersController : ApiController
    {
        private ParkingEntities db = new ParkingEntities();
        UsersModel usersModel = new UsersModel();
        public String mensaje = " ";

        //Servicio para obtener todos los usuarios registrados.
        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return usersModel.SelectAll();
        }

        //Servicio para agregar un registro a la tabla user
        // POST: api/Users
        
        [ResponseType(typeof(String))]
        public IHttpActionResult PostUser(UserRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userRequest.operacion) {
                mensaje = usersModel.InsertUser(userRequest);

                if (mensaje == "OK")
                {
                    mensaje = "INSERT OK";
                }
             }
            else
            {
                mensaje= usersModel.GetByUsernameAndPassUser(userRequest);

                if (mensaje == "NOT_FOUND")
                {
                    return NotFound();
                }

            }

            return Ok(mensaje);
            //return CreatedAtRoute("DefaultApi", new { id = user.id }, user);

        }
        

        
        // PUT: api/Users/5
        [ResponseType(typeof(String))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.id)
            {
                return BadRequest();
            }
            mensaje = usersModel.UpdateUser(id, user);
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

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = usersModel.GetByIdUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(String))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = usersModel.GetByIdUser(id);
            if (user == null)
            {
                return NotFound();
            }
            mensaje = usersModel.RemoveUser(user);
            if (mensaje == "OK")
            {

                return Ok("Delete ok");
            }
            return NotFound();
        }
    }
}