using Interface;
using System.Web.Mvc;
using System;
using Common.Helpers;
using Web.Helpers;
using Entities.Models;

namespace Web.Areas.Management.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool useLDAP = true;
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
        public string GetTemplate(string key)
        {
            var config = _repository.GetRepository<MauVanBan>().Read(x => x.Ma == key);
            return config == null ? "" : config.NoiDung;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[SessionEnum.Email] == null || Session[SessionEnum.Email].ToString() == "")
            {
                System.Web.HttpContext.Current.Response.RedirectToRoute("Login");
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            ViewBag.Error = TempData["Error"];
            ViewBag.Success = TempData["Success"];
            ViewBag.IDDVFT = IdDonViFt;
        }
        public bool LaCapCty
        {
            get
            {
                return CapQuanLy == "CTy";
            }
        }
        public string CapQuanLy
        {
            get
            {
                if (Session[SessionEnum.AccountCQL]!=null)
                {
                    return Session[SessionEnum.AccountCQL].ToString();
                }
                else
                {
                    return "";
                }       
            }
        }
        public long IdDonVi
        {
            get
            {
                long result = -1;
                if (Session[SessionEnum.AccountDv]==null)
                {
                    return result;
                }
                else
                {
                    string val = Session[SessionEnum.AccountDv].ToString();
                    if (long.TryParse(val, out result)) return result;
                    return -1;
                }
                
            }
        }
        public long? IdDonViFt
        {
            get
            {
                
                
                if (CapQuanLy.Equals("CTY"))
                {
                    return null;
                }
                if (Session[SessionEnum.AccountDv] == null)
                {
                    return null;
                }
                else
                {
                    long? result = null;
                    string val = Session[SessionEnum.AccountDv].ToString();
                    long mix = -1;
                    if (long.TryParse(val, out mix))
                    {
                        result = mix;
                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
                

            }
        }
        public string AccountName
        {
            get
            {
                object objAccountName = Session[SessionEnum.AccountName];
                if (objAccountName == null) return "unnamed";
                return objAccountName.ToString();
            }
            set { Session[SessionEnum.AccountId] = value; }
        }
        public long AccountId
        {
            get
            {
                object objAccountId = Session[SessionEnum.AccountId];
                long _AccountId = -1;
                if (objAccountId == null)
                    _AccountId = -1;
                else
                    _AccountId = Convert.ToInt64(objAccountId.ToString());
                return _AccountId;
            }
            set { Session[SessionEnum.AccountId] = value; }
        }//Session[SessionEnum.IsManageAccount] = account.IsManageAccount;
        public bool LaChuyenGia
        {
            get
            {
                object objLaChuyenGia = Session[SessionEnum.IsExpertsAccount];
                bool _LaChuyenGia = false;
                if (objLaChuyenGia == null || objLaChuyenGia == "")
                    _LaChuyenGia = false;
                else
                    _LaChuyenGia = Convert.ToBoolean(objLaChuyenGia.ToString());
                return _LaChuyenGia;
            }
            set { Session[SessionEnum.IsExpertsAccount] = value; }
        }
        public bool LaQuanTriHeThong
        {
            get
            {
                object objLaQuanTriHeThong = Session[SessionEnum.IsManageAccount];
                bool _LaQuanTriHeThong = false;
                if (objLaQuanTriHeThong == null || objLaQuanTriHeThong == "")
                    _LaQuanTriHeThong = false;
                else
                    _LaQuanTriHeThong = Convert.ToBoolean(objLaQuanTriHeThong.ToString());
                return _LaQuanTriHeThong;
            }
            set { Session[SessionEnum.IsManageAccount] = value; }
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