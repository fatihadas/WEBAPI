using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using WEBAPI.Data.Context;
using WEBAPI.Data.Model;
using WEBAPI.Data.Repositories;

namespace WEBAPI.WebApiOAuth.Controllers
{

    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private static ApiContext apiContext = new ApiContext();
        private IRepository<User> userRepository = new EFRepository<User>(apiContext);

        [HttpGet]
        [Route("getall")]
        [Authorize]
        public HttpResponseMessage Users()
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(userRepository.GetAll()));//apiContext.Users.ToList() //userRepository.GetAll().ToList()
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("get/{username}")]
        public HttpResponseMessage find(string username)
        {
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StringContent(JsonConvert.SerializeObject(
                    userRepository.GetAll().Where(p => p.Username == username).Single()));//1-çalışıyor

                //result.Content = new StringContent(JsonConvert.SerializeObject(
                //    userRepository.GetById(webApiContext.Users.Single(p => p.Username == username).Id)));//2-çalışıyor
                //result.Content = new StringContent(JsonConvert.SerializeObject(
                //    webApiContext.Users.Single(p => p.Username == username)));//3-çalısıyor

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        
    }
}
