using Entities.Models;
using Interface;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Helpers;
using System;
using System.Linq;
using Entities.ViewModels;
using System.Text;
using Entities.Models.SystemManage;
namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("chia-se")]
    public class SharedController : Controller
    {
        #region Private
        IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        private long AccountId
        {
            get
            {
                object objAccountId = Session["AccountId"];
                long _AccountId = -1;
                if (objAccountId == null)
                    _AccountId = -1;
                else
                    _AccountId = Convert.ToInt64(objAccountId.ToString());
                return _AccountId;
            }
            set { Session["AccountId"] = value; }
        }
        #endregion
        public ActionResult PublicHeader()
        {
            return PartialView("_PublicHeader");
        }
        public ActionResult HeadNotif()
        {
            var ifNotif = 0;
            if (Session[SessionEnum.Email]!=null)
            {
                string email = Session[SessionEnum.Email].ToString();
                ifNotif = _repository.GetRepository<ThongBao>().GetAll(o => (!o.ThoiGianXem.HasValue || !o.DaXem) && o.An == false && o.Email.Equals(email)).Count();
            }
            return PartialView("_Header", ifNotif);
        }

        #region Thông báo lỗi
        [Route("~/quan-ly/khong-co-quyen-truy-cap", Name = "SharedNoPermission")]
        [ActionName("NoPermission")]
        public ActionResult NoPermission()
        {
            return View();
        }
        [Route("~/quan-ly/loi-he-thong", Name = "ManagementGeneralError")]
        public ActionResult GeneralError()
        {
            ViewBag.Exception = TempData["Exception"];
            return View();
        }
        #endregion
    }
}