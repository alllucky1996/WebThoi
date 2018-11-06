using Entities.Enums;
using System;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Management.Helpers;
namespace Web.Areas.Management.Filters
{
    /// <summary>
    /// Kiểm tra quyền truy cập tới module
    /// </summary>
    public class ValidationPermission : ActionFilterAttribute
    {
        public ModuleEnum Module { get; set; }
        public ActionEnum Action { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Nếu chưa đăng nhập thì bắt đăng nhập
            if (System.Web.HttpContext.Current.Session["Email"] == null || System.Web.HttpContext.Current.Session["Email"].ToString() == "")
            {
                //System.Web.HttpContext.Current.Response.RedirectToRoute("Login");
                filterContext.Result = new RedirectToRouteResult("Login", null);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                //Kiểm tra đã được phân quyền truy cập module hay chưa
                var result = RoleHelper.CheckPermission(Module, Action);
                if (!result)
                    try
                    {
                        //HttpContext.Current.Response.RedirectToRoute("SharedNoPermission");
                        //HttpContext.Current.Response.Redirect("/khong-co-quyen-truy-cap", false);
                        //HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //var routeData = new RouteData();
                        //routeData.Values["controller"] = "Shared";
                        //routeData.Values["action"] = "NoPermission";
                        //routeData.DataTokens["area"] = "Management";
                        //IController controller = new SharedController();

                        //var rc = new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData);
                        //controller.Execute(rc);

                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "Management" }, { "controller", "Shared" }, { "action", "NoPermission" } });

                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        //{
                        //    action = "NoPermission",
                        //    controller = "Shared",
                        //    area = "Management"
                        //}));
                        filterContext.Result = new RedirectToRouteResult("SharedNoPermission", null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        HttpContext.Current.Response.End();
                    }
            }
        }
    }
}