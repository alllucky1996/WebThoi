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

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("dang-nhap")]
    public class LoginController : Controller
    {
        public IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "Login")]
        [ActionName("Index")]
        public ActionResult Login()
        {
            var a = Init("dung");
            ViewBag.Error = TempData["Error"];
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Forget = TempData["Forget"];
            Session.Abandon();
            return View();
        }
        [Route("~/dang-xuat-quan-ly", Name = "LogoutManagerment")]
        public ActionResult Logout()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            ViewBag.Forget = TempData["Forget"];
            Session.Abandon();
            Session.Clear();
            return RedirectToRoute("ManagementHome");
        }
        /// <summary>
        /// Đăng nhập hệ thống (POST)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
             
            string errorStr = "";
            string msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string email = StringHelper.KillChars(model.Email);
                    string password = StringHelper.stringToSHA512(StringHelper.KillChars(model.Password)).ToLower();
                   // sửa truy vấn này vs fornt end 
                    var account = await _repository.GetRepository<Account>().ReadAsync(o => (o.Email == email && o.Password == password && o.IsManageAccount == true));
                  
                    Session.Clear();

                    if (account == null)
                    {
                        ModelState.AddModelError(string.Empty, "Sai địa chỉ e-mail hoặc mật khẩu!");
                        ViewBag.Error = "Sai địa chỉ e-mail hoặc mật khẩu!";
                        return View(model);
                    }
                    else
                    {
                        
                        Session[SessionEnum.Email] = account.Email;
                        Session[SessionEnum.AccountId] = account.Id;
                        Session[SessionEnum.AccountName] = account.FullName;
                        Session[SessionEnum.ProfilePicture] = account.ProfilePicture;
                        Session[SessionEnum.IsExpertsAccount] = account.IsExpertsAccount;
                        Session[SessionEnum.IsManageAccount] = account.IsManageAccount;
                        Session[SessionEnum.AccountType] = "Admin";
                        Session[SessionEnum.AccountDv] = account.IdDonVi;
                        Session[SessionEnum.AccountCQL] = account.CapQuanLy;
                        Session[SessionEnum.IdNS] = "";
                        //Session["Message"] = msg;
                        //ViewBag.Error = msg;
                        //return View(model);
                        var accountRoles = await _repository.GetRepository<AccountRole>().GetAllAsync(o => o.AccountId == account.Id);
                        var moduleRoles = await _repository.GetRepository<ModuleRole>().GetAllAsync();
                        CacheFactory _cacheFactory = new CacheFactory();
                        _cacheFactory.RemoveCache("AccountRoles_" + account.Id);
                        _cacheFactory.RemoveCache("ModuleRoles");
                        _cacheFactory.SaveCache("AccountRoles_" + account.Id, accountRoles.ToList());
                        _cacheFactory.SaveCache("ModuleRoles", moduleRoles.ToList());
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    //ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi: " + ex.Message);
                    ViewBag.Error = "Đã có lỗi xảy ra: " + ex.Message;
                    return View(model);
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Vui lòng nhập đúng thông tin để đăng nhập!");
                ViewBag.Error = "Vui lòng nhập đúng thông tin để đăng nhập!";
                return View(model);
            }
        }
        /// <summary>
        /// Khởi tạo dữ liệu khi lần đầu tiên chạy trang web
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("~/quan-ly/khoi-tao/{key?}")]
        public async Task<ActionResult> Init(string key)
        {
            if (!key.Equals("dung"))
            {
                return RedirectToAction("Index", "Login");
            }
            // account
            if (_repository.GetRepository<Account>().Any(o => o.Id == 1))
            {
                return RedirectToAction("Index", "Login");
            }
            var account = new Account()
            {
                FullName = "Nguyễn Anh Dũng",
                Password = StringHelper.stringToSHA512("123456").ToLower(),
                Email = "itfa.ahihi@gmail.com",
                CreateDate = DateTime.Now,
                IsManageAccount = true,
                IsNormalAccount = false,
                PhoneNumber = "0978132474"
            };
            await _repository.GetRepository<Account>().CreateAsync(account, 0);
            // role
            var role = new Role()
            {
                Name = "Quản trị hệ thống"
            };
            await _repository.GetRepository<Role>().CreateAsync(role, 1);
            //gán quyền
            var accountRole = new AccountRole()
            {
                AccountId = 1,
                RoleId = 1
            };
            await _repository.GetRepository<AccountRole>().CreateAsync(accountRole, 1);
            // app module quyền
            var moduleRole = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "Account",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(moduleRole, 1);
            var moduleRole2 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "Role",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(moduleRole2, 1);
            var moduleRole3 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "DonVi",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(moduleRole3, 1);
            var si = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "SystemInformation",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(si, 1);
            
            return RedirectToAction("Index", "Login");
        }
       
    }
}