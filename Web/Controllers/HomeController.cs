using Common.Helpers;
using Entities.Models.SystemManage;
using Interface;
using Microsoft.Office.Core;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        [Route(Name = "FrontEndHomeIndex")]
        public ActionResult Index()
        {
            return View();
            //return RedirectToAction("Index", "Home", new { area = "FrontEnd" });
        }

        public ActionResult BieuMau()
        {
            return RedirectToRoute("BieuMau");
        }

        //Khởi tạo dữ liệu ban đầu
        public ActionResult KhoiTao()
        {
            string status="";
            //Khởi tạo danh mục loại Mục tin
            //Khởi tạo danh mục Trạng thái bài viết
            return View(status);
        }
    }
}