using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WEBAPI.WebApiOAuth.OAuth.Providers;

[assembly: OwinStartup(typeof(WEBAPI.WebApiOAuth.OAuth.Startup))]
namespace WEBAPI.WebApiOAuth.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();

            ConfigureOAuth(appBuilder);

            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"), // token alacağımız path
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),// token expire time
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider()
            };
            
            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}