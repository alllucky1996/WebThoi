using Entities.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class HomeController : BaseController
    {
        
        [Route(Name = "ManagementHome")] 
        public ActionResult Index()
        {
            var models = _repository.GetRepository<ThongBao>().GetAll().ToList();

            string tieuDe = GetTemplate("TIEUDE_YEUCAU");

            return View(models);
        }

        [Route("xem-notif/{id?}", Name = "ManagementNotifView")]
        public ActionResult ViewNotif(long id)
        {
            var models = _repository.GetRepository<ThongBao>().Read(o => o.Id == id);

            if (models.DaXem==false)
            {
                models.DaXem = true;
            }
            if (!models.ThoiGianXem.HasValue)
            {
                models.ThoiGianXem = DateTime.Now;
            }
            

            _repository.GetRepository<ThongBao>().Update(models, AccountId);

            return PartialView("_NotifView",models);
        }

        [Route("danh-sach-notif", Name = "ManagementNotif")]
        public ActionResult NotifJson()
        {
            var dataChuaXem = _repository.GetRepository<ThongBao>().GetAll(o => o.AccountId==AccountId && o.An==false && (o.DaXem == false || !o.ThoiGianXem.HasValue)).OrderByDescending(o => o.ThoiGianGui).ToList().Select(o => new {
                o.Id,
                o.Ma,
                o.Email,
                ThoiGianGui = o.ThoiGianGui.ToString("dd/MM/yyyy"),
                ThoiGianXem = o.ThoiGianXem.HasValue ? o.ThoiGianXem.Value.ToString("dd/MM/yyyy") : "",
                o.DaXem,
                o.CoGuiEmail,
                o.DaGuiEmail,
                o.TieuDe,
                o.NoiDung,
                o.NguoiGui
            }).ToList();

            var dataDaXem = _repository.GetRepository<ThongBao>().GetAll(o => o.AccountId == AccountId && o.An == false && (o.DaXem == true && o.ThoiGianXem.HasValue)).OrderByDescending(o => o.ThoiGianGui).ToList().Select(o => new {
                o.Id,
                o.Ma,
                o.Email,
                ThoiGianGui = o.ThoiGianGui.ToString("dd/MM/yyyy"),
                ThoiGianXem = o.ThoiGianXem.HasValue ? o.ThoiGianXem.Value.ToString("dd/MM/yyyy") : "",
                o.DaXem,
                o.CoGuiEmail,
                o.DaGuiEmail,
                o.TieuDe,
                o.NoiDung,
                o.NguoiGui
            }).ToList();

            return Json(new
            {
                dataDX = dataDaXem,
                dataCX = dataChuaXem
            }, JsonRequestBehavior.AllowGet);
        }
    }
}