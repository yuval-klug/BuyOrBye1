using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
<<<<<<< HEAD
=======
using System.Web.Http.Cors;
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628

namespace BuyOrBye
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
<<<<<<< HEAD
=======
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
>>>>>>> d6ea44552777b9a58bcf474e90ce61378d55f628

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
