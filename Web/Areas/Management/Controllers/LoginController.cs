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
using Dung.Model;
using Dung;
using System.Diagnostics;
using Dung.Enums;

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
            //Init("namdung");
            //InitKhachHang();
            //InitMucTin();
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
                    //password = StringHelper.StringToMd5(password);

                    var account = await _repository.GetRepository<Account>().ReadAsync(o => (o.Email == email && o.Password == password));
                    //var ns = await _repository.GetRepository<nsNhanSu>().ReadAsync(o => (o.Email == email&&ldapValidate));

                    Session.Clear();

                    if (account == null)
                    {
                        //ModelState.AddModelError(string.Empty, "Sai địa chỉ e-mail hoặc mật khẩu!");

                        ViewBag.Error = "Sai địa chỉ e-mail hoặc mật khẩu!";

                        return View(model);

                    }
                    else
                    {

                        Session[SessionEnum.Email] = account.Email;
                        Session[SessionEnum.AccountId] = account.Id;
                        Session[SessionEnum.AccountName] = account.Name;
                        Session[SessionEnum.ProfilePicture] = account.ProfilePicture;
                        Session[SessionEnum.IsExpertsAccount] = account.IsExpertsAccount;
                        Session[SessionEnum.IsManageAccount] = account.IsManageAccount;
                        Session[SessionEnum.AccountType] = "Admin";
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
            if (key != "namdung")
            {
                return RedirectToAction("Index", "Login");
            }
            if (_repository.GetRepository<Account>().Any(o => o.Id == 1))
            {
                return RedirectToAction("Index", "Login");
            }
            var account = new Account()
            {
                Name = "Administrator",
                Password = StringHelper.stringToSHA512("123456").ToLower(),
                Email = "itfa.ahihi@gmail.com",
                CreateDate = DateTime.Now,
                IsManageAccount = true,
                IsNormalAccount = false,
                PhoneNumber = ""
            };
            await _repository.GetRepository<Account>().CreateAsync(account, 0);
            var role = new Role()
            {
                Name = "Quản trị hệ thống"
            };
            await _repository.GetRepository<Role>().CreateAsync(role, 1);
            var role2 = new Role()
            {
                Name = "Quản trị danh mục"
            };
            await _repository.GetRepository<Role>().CreateAsync(role2, 1);
            var role3 = new Role()
            {
                Name = "Quản trị nội dung"
            };
            await _repository.GetRepository<Role>().CreateAsync(role3, 1);
            var accountRole = new AccountRole()
            {
                AccountId = 1,
                RoleId = 1
            };
            var accountRole2 = new AccountRole()
            {
                AccountId = 1,
                RoleId = 2
            };
            var accountRole3 = new AccountRole()
            {
                AccountId = 1,
                RoleId = 3
            };
            await _repository.GetRepository<AccountRole>().CreateAsync(accountRole, 1);
            await _repository.GetRepository<AccountRole>().CreateAsync(accountRole2, 1);
            await _repository.GetRepository<AccountRole>().CreateAsync(accountRole3, 1);
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
            var dmbv = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "TrangThaiBaiViet",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv, 1);
            var dmbv2 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "ThaoTacChuyenTrangThai",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv2, 1);
            var dmbv3 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "TaoBaiViet",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv3, 1);
            var dmbv4 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "DuyetBaiViet",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1,
                Verify = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv4, 1);
            var dmbv5 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "XuLyBaiViet",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1,
                Verify = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv5, 1);
            var dmbv6 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "MucTin",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1,
                Verify = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv6, 1);
            var dmbv7 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "KhachHang",
                Create = 1,
                Read = 1,
                Update = 0,
                Delete = 1,
                Verify = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(dmbv7, 1);
           
           // InitKhachHang();
          

            return RedirectToAction("Index", "Login");

        }
        /// <summary>
        /// Yêu cầu mật khẩu mới khi quên mật khẩu
        /// </summary>
        /// <returns></returns>
        [Route("~/quen-mat-khau/")]
        [HttpPost]
        public async Task<ActionResult> ForgetPassword()
        {
            TempData["Forget"] = true;
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// Xác nhận yêu cầu mật khẩu mới
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("~/xac-nhan-khoi-phuc-mat-khau/{code?}")]
        public async Task<ActionResult> ConfirmPassword(string code)
        {
             return View();
        }
       
       
        //// khởi tạo khách hàng
        //void InitKhachHang()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var kh = new KhachHang()
        //        {
        //            Email = "email00" + i.ToString() + "@gmail.com",
        //            Name = "Name00" + i.ToString(),
        //            NhuCau = "mk xin bảng giá và dánh sách căn hộ trống hiện tại",
        //            PhoneNumber = "01627835923",
        //            NgayTao = DateTime.Now,
        //            TrangThaiKhachHang = TrangThaiKhachHangEnum.ChuaLienHe
        //        };
        //        try
        //        {
        //            var rp = _repository.GetRepository<KhachHang>().Create(kh, 0);
        //            if (rp > 0) Debug.WriteLine("khởi tạo thành công" + kh.Name);
        //            else
        //            {
        //                Debug.WriteLine("Không khởi tạo được" + kh.Name);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.Message);
        //        }
        //    }
        //}


    }
}