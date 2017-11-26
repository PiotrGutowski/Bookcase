using Bookcase.WebClient.Controllers;
using NLog;
using NLog.Fluent;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bookcase.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
          
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
           Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal(exc);
            //Server.ClearError();
        }
        
    }

}
