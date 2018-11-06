using Interface;
using System.Web.Mvc;
using System;
using Common.Helpers;
using Web.Helpers;
using Entities.Models;

namespace Web.Areas.FrontEnd.Controllers
{
    public abstract class PublicController : Controller
    {
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
        
        protected readonly long AccountId =0;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
        public string GetTemplate(string key)
        {
            var config = _repository.GetRepository<MauVanBan>().Read(x => x.Ma == key);
            return config == null ? "" : config.NoiDung;
        }
       
        public static string ControllerName()
        {
            return "";
        }

        public ActionResult _PrintPreview(object model, string fileName, string viewName)
        {
            ViewBag.DocType = "word";
            return new WordResult(
                ControllerContext,
                viewName,
                fileName,
                model
            );
            //return PartialView("_");
        }
        public ActionResult _PrintPreview(object model, string fileName, string viewName, ViewDataDictionary viewData)
        {
            ViewBag.DocType = "word";
            return new WordResult(
                ControllerContext,
                viewName,
                fileName,
                model,
                viewData
            );
            //return PartialView("_");
        }
    }
}