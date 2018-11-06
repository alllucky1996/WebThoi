using System.Web.Mvc;

namespace Web.Areas.FrontEnd
{
    public class FrontEndAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FrontEnd";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FrontEnd_default",
                "FrontEnd/{controller}/{action}/{id}",
                 new { controller = "AccountService", action = "Index", id = UrlParameter.Optional },
                new[] { "Web.Areas.FrontEnd.Controllers" }
            );
        }
    }
}