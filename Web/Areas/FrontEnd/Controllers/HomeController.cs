using Entities.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Entities.Models.ThongKe;
using System.Threading.Tasks;
using Entities.Enums;
using Web.Areas.Management.Filters;
using Common.Helpers;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Dung.Model;
using System.Web;

namespace Web.Areas.FrontEnd.Controllers
{
    [RouteArea("FrontEnd", AreaPrefix = "dich-vu")]
    public class HomeController : PublicController
    {
        public const string CName = "HinhAnh";
        public const string CRoute = "hinh-anh";
        public const string CText = " Hình ảnh";
        #region index
        [Route(Name = "FrontEnd_Home_Index")]
        public  ActionResult Index()
        {
            try
            {
                var list = _repository.GetRepository<CheckImage>().GetAll(o=>o.IsDeleted!= true);
                ViewBag.Title = "Danh mục" + CText;
                
                ViewBag.CanCreate= true;
                ViewBag.CName = CName;
                ViewBag.CText = CText;
                return View(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion


        #region Action
        [Route("nhap-" + CRoute, Name = CName + "_MultiCreate")]
        public async Task<ActionResult> MultiCreate()
        {
            ViewBag.Title = "Thêm mới " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View();
        }
        [Route("nhap" + CRoute, Name = CName + "_Create")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Title = "Thêm mới " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View();
        }
        [Route("nhap" + CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CheckImage model, HttpPostedFileBase file)
        {
            try
            {
                Guid id = Guid.NewGuid();
               
                if (file == null)
                {
                    return Json(new { success = false, message = "Không có file gửi lên" }, JsonRequestBehavior.AllowGet);
                }
                var type = Path.GetExtension(file.FileName);
                var  fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Guid.NewGuid().ToString().Replace("-","_") + type;
                var temp = Server.MapPath("~/Upload/CheckImage/") + fileName;
                file.SaveAs(temp);
                model.Path = "/Upload/CheckImage/" + fileName;
                //Nhập trạng thái bài viết
                var newItem = new CheckImage();
                newItem.Code = StringHelper.KillChars(model.Code);
                newItem.Name = model.Name == null ? Path.GetFileName(fileName): model.Name;
                newItem.Description = model.Description;
                newItem.Path = model.Path;
                newItem.IsChecked = model.IsChecked;

                int resul = _repository.GetRepository<CheckImage>().Create(newItem, AccountId);
                if (resul > 0)
                {
                    TempData["Success"] = "Thêm mới thành công " + CText;
                    //return Json(new { success = true, message = TempData["Success"], result = resul }, JsonRequestBehavior.AllowGet);
                      return RedirectToRoute(CName + "_Index");
                }
                else
                {
                    ViewBag.Error = "Không thêm được " + CText;
                    //return Json(new { success = false, message = "Không thêm được " + CText, result = resul }, JsonRequestBehavior.AllowGet);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                return View(model);
            }
        }
        #endregion

    }
}