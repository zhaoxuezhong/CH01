using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CH01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "CH01.Controllers" }
            );

            routes.MapRoute(
                name: "bookdetail",
                url: "Books/Detail/Static/{id}.html",
                defaults: new { controller = "Books", action = "Detail"},
                namespaces: new string[] { "CH01.Controllers" }
            );
        }
    }
}