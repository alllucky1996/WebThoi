using Dung.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class HomeController : BaseController
    {
        public  ActionResult  Index()
        {
           // var list = _repository.GetRepository<TuyenDung>().GetAll();
            //list.ToList().Where(o => o.IsDeleteed != true);
            //  return View(list);
            return View();
        }
        public ActionResult Delete( string id)
        {
          //  var item = _repository.GetRepository<TuyenDung>().Read(long.Parse(id));
          //  item.IsDeleteed = true;
          //  item.TimeUpdate = DateTime.Now;
           // var ok = _repository.GetRepository<TuyenDung>().Update(item, 0);
            return new JsonResult();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> TuyenDung()
        {
            // _reponsitory.getReponsirory<TuyenDung>().getAllAsync();
            return View();
        }
        public async Task<ActionResult> NhaTuyenDung()
        {
            // _reponsitory.getReponsirory<TuyenDung>().getAllAsync();
            return View();
        }
        
    }
}