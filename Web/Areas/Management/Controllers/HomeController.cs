using Dung.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class HomeController : BaseController
    {
        [Route(Name = "ManagementHome")]
        public async Task<ActionResult> Index(String Code)
        {
            if (Code == "dung")
            {
                InitDB();
                return Json(new { success = true, message = "Khỏi tạo 'Images' thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        void InitDB()
        {
            for (int i = 1; i < 100; i++)
            {
                var ni = new CheckImage() {Code= i.ToString(),Name="Hình 001",Path="#",Description= "Mô tả hình Hình 001" ,IsChecked=(i%2==0?true:false) };
                _repository.GetRepository<CheckImage>().Create(ni, 0);
            }
        }

       
    }
}