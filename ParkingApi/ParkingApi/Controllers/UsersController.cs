
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

        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        UsersModel usersModel = new UsersModel();
        public String mensaje = " ";

        [HttpPost]
        [POST("api/Users/login")]
        public IHttpActionResult checkLogin(UserRequest userReq)
        {
            //string username = System.Web.HttpContext.Current.Request.QueryString.Get("username");

            User user = usersModel.GetByUsernameUser(userReq.username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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
    }
}