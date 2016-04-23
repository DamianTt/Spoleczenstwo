using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Beng",
                url: "beng/{id}",
                defaults: new { controller = "Post", action = "Details" }
            );

            routes.MapRoute( 
                name: "Home", 
                url: "Home",
                defaults: new { controller = "Post", action = "Index" }
            );

            routes.MapRoute(
                name: "Manage", 
                url: "Manage",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Sections",
                url: "{sectionName}",
                defaults: new { controller = "Post", action = "Section" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
