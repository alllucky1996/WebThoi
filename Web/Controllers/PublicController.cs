using System.Web.Mvc;

namespace Web.Controllers
{
    public class PublicController : Controller
    {
       [Route("~/loi", Name = "FrontEndPublicIndex")]
        public ActionResult Index()
        {
            ViewBag.Exception = TempData["Exception"];
            return View();
        }
    }
}