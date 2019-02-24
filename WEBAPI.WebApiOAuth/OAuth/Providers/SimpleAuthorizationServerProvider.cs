using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WEBAPI.Data.Context;
using WEBAPI.Data.Model;
using WEBAPI.Data.Repositories;

namespace WEBAPI.WebApiOAuth.OAuth.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static ApiContext webApiContext = new ApiContext();
        private IRepository<User> userRepository = new EFRepository<User>(webApiContext);
        
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext gelencontext)
        {
            gelencontext.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });// CORS
            

            bool basarili = (userRepository.GetAll().Where(p => p.Username == gelencontext.UserName &&
                p.Password == gelencontext.Password).Count() > 0) ? true : false;
            if (basarili)
            {
                var identity = new ClaimsIdentity(gelencontext.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", gelencontext.UserName));
                identity.AddClaim(new Claim("role", "user"));
                gelencontext.Validated(identity);
            }
            else
            {
                gelencontext.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
            }

        }
    }
}