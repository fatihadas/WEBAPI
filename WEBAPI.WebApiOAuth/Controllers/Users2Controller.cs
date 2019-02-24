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
using WEBAPI.Data.Context;
using WEBAPI.Data.Model;
using WEBAPI.Data.Repositories;

namespace WEBAPI.WebApiOAuth.Controllers
{
    [RoutePrefix("api/users2")]
    public class Users2Controller : ApiController
    {
        private static ApiContext db = new ApiContext();
        private IRepository<User> userRepository = new EFRepository<User>(db);
        
        [HttpGet]
        [Route("getall")]
        [Authorize]
        public IQueryable<User> GetUsers()
        {
            return userRepository.GetAll();// db.Users;
        }
        
        [HttpGet]
        [Authorize]
        [Route("getid/{id}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserId(int id)
        {
            try
            {
                User article = userRepository.GetAll().Where(p => p.Id == id).Single();// db.Users.Find(id);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(article);
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Authorize]
        [Route("getusername/{username}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserName(string username)
        {
            try
            {
                User article = userRepository.GetAll().Where(p => p.Username == username).Single();// db.Users.Find(id);
                if (article == null)
                {
                    return NotFound();
                }
                return Ok(article);
            }
            catch
            {
                return NotFound();
            }
            
        }
        

    }
}