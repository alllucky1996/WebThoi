using Entities.Enums;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Helpers;
using Entities.Models.SystemManage;
using Web.Areas.Management.Filters;
using Web.Areas.Management.Helpers;
namespace Web.Areas.Management.Controllers
{
    /// <summary>
    /// Cấu hình các tham số của hệ thống
    /// </summary>
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("tham-so-he-thong")]
    public class SystemInformationController : BaseController
    {
        [Route("", Name = "SystemInformationIndex")]
        [ValidationPermission(Action = ActionEnum.Read, Module = ModuleEnum.SystemInformation)]
        public async Task<ActionResult> Index()
        {
            SystemInformation systemInformation = (await _repository.GetRepository<SystemInformation>().GetAllAsync()).FirstOrDefault();
            if (systemInformation == null)
            {
                systemInformation = new SystemInformation()
                {
                    Id = 0,
                    SMTPPassword = ""
                };
            }
            systemInformation.SMTPPassword = Base64Hepler.DecodeFrom64(systemInformation.SMTPPassword);
            try
            {
                string filePath = Server.MapPath("~/Uploads/images/chung/");
                if (!Directory.Exists(filePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }
            }
            catch { }
            return View(systemInformation);
        }
        [Route("")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.SystemInformation)]
        public async Task<ActionResult> Index(SystemInformation model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var systemInformation = (await _repository.GetRepository<SystemInformation>().GetAllAsync()).FirstOrDefault();
                    if (systemInformation == null)
                    {
                        systemInformation = new SystemInformation();
                        systemInformation.SiteName = StringHelper.KillChars(model.SiteName);
                        systemInformation.Slogan = StringHelper.KillChars(model.Slogan);
                       
                        systemInformation.Copyright = StringHelper.KillChars(model.Copyright);
                        systemInformation.Email = StringHelper.KillChars(model.Email);

                        systemInformation.CompanyName = StringHelper.KillChars(model.CompanyName);
                        systemInformation.Address = StringHelper.KillChars(model.Address);
                        systemInformation.HotLine = StringHelper.KillChars(model.HotLine);
                        systemInformation.PhoneNumber = StringHelper.KillChars(model.PhoneNumber);
                        systemInformation.WebsiteAddress = StringHelper.KillChars(model.WebsiteAddress);
                        systemInformation.FacebookPage = StringHelper.KillChars(model.FacebookPage);
                        systemInformation.FacebookAppId = StringHelper.KillChars(model.FacebookAppId);
                        systemInformation.TaxiFare = model.TaxiFare;
                        int result = await _repository.GetRepository<SystemInformation>().CreateAsync(systemInformation, AccountId);
                    }
                    else
                    {
                        systemInformation.SiteName = StringHelper.KillChars(model.SiteName);
                        systemInformation.Slogan = StringHelper.KillChars(model.Slogan);
                        
                        systemInformation.Copyright = StringHelper.KillChars(model.Copyright);
                        systemInformation.Email = StringHelper.KillChars(model.Email);

                        systemInformation.CompanyName = StringHelper.KillChars(model.CompanyName);
                        systemInformation.Address = StringHelper.KillChars(model.Address);
                        systemInformation.HotLine = StringHelper.KillChars(model.HotLine);
                        systemInformation.PhoneNumber = StringHelper.KillChars(model.PhoneNumber);
                        systemInformation.WebsiteAddress = StringHelper.KillChars(model.WebsiteAddress);
                        systemInformation.FacebookPage = StringHelper.KillChars(model.FacebookPage);
                        systemInformation.FacebookAppId = StringHelper.KillChars(model.FacebookAppId);
                        systemInformation.TaxiFare = model.TaxiFare;
                        int result = await _repository.GetRepository<SystemInformation>().UpdateAsync(systemInformation, AccountId);
                    }
                    //Session["SystemInformation"] = systemInformation;
                    _cacheFactory.SaveCache("SystemInformation", systemInformation);
                    ViewBag.Success = "Đã ghi nhận thành công!";

                    return View(systemInformation);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập chính xác thông tin!");
                ViewBag.Error = "Vui lòng nhập chính xác thông tin!";
                return View(model);
            }
        }
        [Route("smtp", Name = "SystemInformationSMTP")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.SystemInformation)]
        public async Task<ActionResult> SMTP(SystemInformation model)
        {
            try
            {
                var systemInformation = (await _repository.GetRepository<SystemInformation>().GetAllAsync()).FirstOrDefault();
                if (systemInformation == null)
                {
                    systemInformation = new SystemInformation();
                    systemInformation.SMTPEmail = model.SMTPEmail;
                    systemInformation.SMTPName = model.SMTPName;
                    systemInformation.SMTPPassword = Base64Hepler.EncodeTo64UTF8(model.SMTPPassword);
                    int result = await _repository.GetRepository<SystemInformation>().CreateAsync(systemInformation, AccountId);
                }
                else
                {
                    systemInformation.SMTPEmail = model.SMTPEmail;
                    systemInformation.SMTPName = model.SMTPName;
                    systemInformation.SMTPPassword = Base64Hepler.EncodeTo64UTF8(model.SMTPPassword);
                    int result = await _repository.GetRepository<SystemInformation>().UpdateAsync(systemInformation, AccountId);
                }
                _cacheFactory.SaveCache("SystemInformation", systemInformation);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = "Đã xảy ra lỗi: " + ex.Message;
                return Json(new { success = false, message = message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}