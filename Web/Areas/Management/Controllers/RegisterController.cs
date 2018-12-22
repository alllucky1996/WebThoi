using Entities.ViewModels;
using Interface;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Helpers;
using Entities.Models.SystemManage;
using Entities.Models;
using System.Web;
using System.IO;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("dang-ky")]
    public class RegisterController : Controller
    {
        public IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "Register")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Forget = TempData["Forget"];
            Session.Abandon();
            return View();
        }

        /// <summary>
        /// Đăng nhập hệ thống (POST)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Account model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //Upload
                string photo = "";
                try
                {
                    string fullFilePath = "/Uploads/images/anh-nguoi-dung/";
                    string filePath = Server.MapPath("~/Uploads/images/anh-nguoi-dung/");
                    try
                    {
                        if (!Directory.Exists(filePath))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(filePath);
                        }
                    }
                    catch { }
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = CommonHelper.ToURL(Path.GetFileNameWithoutExtension(file.FileName), 0) + Path.GetExtension(file.FileName);
                        var path = Path.Combine(filePath, fileName);
                        if (System.IO.File.Exists(path))
                        {
                            fileName = string.Format("{0}_{1}{2}", CommonHelper.ToURL(Path.GetFileNameWithoutExtension(file.FileName), 0), String.Format("{0:dd_MM_yyyy_hh_mm_ss_fff}", DateTime.Now), Path.GetExtension(file.FileName));
                            path = Path.Combine(filePath, fileName);
                        }
                        file.SaveAs(path);
                        fullFilePath = fullFilePath + fileName;
                        photo = fullFilePath;
                    }
                }
                catch { }

                string email = StringHelper.KillChars(model.Email);
                //Kiểm tra trùng email
                
                bool exists = await _repository.GetRepository<Account>().AnyAsync(o => o.Email == email);

                if (exists)
                {
                    ModelState.AddModelError(string.Empty, "Địa chỉ email đã tồn tại!");
                    ModelState.AddModelError("Email", "Địa chỉ email này đã được sử dụng cho tài khoản khác!");
                    return View(model);
                }
                else
                {
                    string password =  StringHelper.KillChars(model.Password).ToLower();
                    Account account = new Account();
                    account.IsManageAccount = false;
                    account.IsNormalAccount = true;
                    account.Email = email;
                    //account.CapQuanLy = "K";
                    account.FullName = StringHelper.KillChars(model.FullName);
                    //account.Code = StringHelper.KillChars(model.Code);
                    account.PhoneNumber = StringHelper.KillChars(model.PhoneNumber);
                    //account.IsExpertsAccount = model.IsExpertsAccount;
                    if (!string.IsNullOrEmpty(photo))
                        account.ProfilePicture = photo;// StringHelper.KillChars(model.ProfilePicture); ;
                    account.CreateDate = DateTime.Now;
                    account.Password = StringHelper.stringToSHA512(password);
                    int result = await _repository.GetRepository<Account>().CreateAsync(account, 1);
                    if (result > 0)
                    {
                        TempData["Success"] = "Nhập tài khoản mới thành công!";
                        return RedirectToRoute("LoginSanGiaoDich");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nhập tài khoản mới không thành công! Vui lòng kiểm tra và thử lại!");
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập chính xác các thông tin!");
                return View(model);
            }
        }
    }
}