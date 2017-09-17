using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParkingApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "login",
                url: "api/Users/login/{username}/{pass}");
            
        }

        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                    name: "Default",
                    routeTemplate: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                    name: "login",
                    routeTemplate: "api/Users/login/{username}/{pass}"
                );
        }
    }
}
