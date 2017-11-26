using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Bookcase.Api.App_Start;
using System.Web.Http.Cors;
using Bookcase.Api.ExLogger;
using System.Web.Http.ExceptionHandling;

namespace Bookcase.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            StructuremapWebApi.Start();
            config.Services.Add(typeof(IExceptionLogger), new ExceptionManagerApi());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

        }
    }
}

