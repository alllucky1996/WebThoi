using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDemo
{
    public class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //     routes.MapRoute(name: "DefaultCaptchaRoute", url: "DefaultCaptcha/Generate", defaults: new { controller = "DefaultCaptcha", action = "Generate" }, namespaces: new[] { "Web.Controllers" });
        //    routes.MapRoute(name: "DefaultCaptchaRouteRefresh", url: "DefaultCaptcha/Refresh", defaults: new { controller = "DefaultCaptcha", action = "Refresh" }, namespaces: new[] { "Web.Controllers" });
        //    routes.MapMvcAttributeRoutes();//Attribute Routing
        //    AreaRegistration.RegisterAllAreas();
        //    routes.MapRoute(
        //        name: "Default",
        //        url: "{controller}/{action}/{id}",
        //        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //        namespaces: new[] { "Web.Controllers" }
        //    );
        //}
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
