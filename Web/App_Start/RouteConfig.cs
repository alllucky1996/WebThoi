using System.Web.Mvc;
using System.Web.Routing;
namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.IgnoreRoute("{file}.css");
            //routes.IgnoreRoute("{file}.js");
            //routes.IgnoreRoute("{file}.jpg");
            //routes.IgnoreRoute("{file}.gif");
            //routes.IgnoreRoute("{file}.png");
            //routes.IgnoreRoute("{file}.ttf");

            //routes.IgnoreRoute("Editor/{*pathInfo}");
            //routes.IgnoreRoute("Content/{*pathInfo}");
            //routes.IgnoreRoute("Images/{*pathInfo}");
            //routes.IgnoreRoute("Scripts/{*pathInfo}");
            //routes.IgnoreRoute("Themes/{*pathInfo}");
            //routes.IgnoreRoute("Uploads/{*pathInfo}");

            //If you want to combine these into one route, you can (e.g., ignore specific types of files in a directory):
            //routes.IgnoreRoute("{assets}", new { assets = @".*\.(css|js|gif|jpg)(/.)?" });

            //routes.MapRoute(name: "DefaultCaptchaRoute1", url: "SuperCaptcha/InitCaptcha", defaults: new { controller = "SuperCaptcha", action = "InitCaptcha" }, namespaces: new[] { "Web.Controllers" });
            //routes.MapRoute(name: "DefaultCaptchaRoute2", url: "SuperCaptcha/NewCaptcha", defaults: new { controller = "SuperCaptcha", action = "NewCaptcha" }, namespaces: new[] { "Web.Controllers" });
            //routes.MapRoute(name: "DefaultCaptchaRoute3", url: "SuperCaptcha/ValidateCaptcha", defaults: new { controller = "SuperCaptcha", action = "ValidateCaptcha" }, namespaces: new[] { "Web.Controllers" });
            routes.MapRoute(name: "DefaultCaptchaRoute", url: "DefaultCaptcha/Generate", defaults: new { controller = "DefaultCaptcha", action = "Generate" }, namespaces: new[] { "Web.Controllers" });
            routes.MapRoute(name: "DefaultCaptchaRouteRefresh", url: "DefaultCaptcha/Refresh", defaults: new { controller = "DefaultCaptcha", action = "Refresh" }, namespaces: new[] { "Web.Controllers" });
            routes.MapMvcAttributeRoutes();//Attribute Routing
            AreaRegistration.RegisterAllAreas();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Web.Controllers" }
            );
        }
    }
}
