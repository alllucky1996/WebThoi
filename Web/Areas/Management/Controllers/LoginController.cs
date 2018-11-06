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
using Entities.Models.ThongKe;
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
            KhoitaoTrangThai();
            InitKhachHang();
            InitMucTin();

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
            if (ModelState.IsValid)
            {
                string email = Request.Params["email_forget"];
                var account = await _repository.GetRepository<Account>().ReadAsync(o => o.Email == email);
                if (account == null)
                {
                    TempData["Error"] = "Địa chỉ email không tồn tại!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    string activeCode = StringHelper.CreateRandomString(32);
                    activeCode = StringHelper.StringToMd5(activeCode).ToLower();
                    var temporaryPassword = StringHelper.CreateRandomString(8);

                    ForgetPassword forgetPassword = new ForgetPassword();
                    forgetPassword.AccountId = account.Id;
                    forgetPassword.ActiveCode = activeCode;
                    forgetPassword.RequestTime = DateTime.Now;
                    forgetPassword.TemporaryPassword = Base64Hepler.EncodeTo64UTF8(StringHelper.StringToMd5(StringHelper.StringToMd5(StringHelper.KillChars(temporaryPassword)).ToLower()));
                    forgetPassword.Status = 0;
                    forgetPassword.RequestIp = CommonHelper.GetIPAddress(Request);
                    int result = await _repository.GetRepository<ForgetPassword>().CreateAsync(forgetPassword, account.Id);
                    var systemInfo = (await _repository.GetRepository<SystemInformation>().GetAllAsync()).FirstOrDefault();

                    if (result > 0 && systemInfo != null)
                    {
                        string domainName = Request.Url.Scheme + "://" + Request.Url.Authority;
                        StringBuilder body = new StringBuilder();
                        body.Append("Kính gửi " + StringHelper.KillChars(account.Name) + ",<br /><br />");
                        body.Append("Quí vị đã yêu cầu khôi phục mật khẩu trên website " + systemInfo.SiteName + "!<br />");
                        body.Append("Mật khẩu mới của quí vị là: " + temporaryPassword);
                        body.Append("<br />Quí vị vui lòng bấm ");
                        body.Append("<a href=\"" + domainName + "/xac-nhan-khoi-phuc-mat-khau/" + activeCode + "\" target=\"_blank\">vào đây</a>");
                        body.Append(" để xác thực việc quên mật khẩu. <br />");
                        body.Append(" Yêu cầu của quí vị chỉ có hiệu lực trong 24 giờ. <br />");
                        body.Append("<br /><br />Vô cùng xin lỗi nếu email này làm phiền quí vị!<br /><br />");
                        body.Append("<br />Kính thư, <br /><br />");
                        body.Append(systemInfo.SiteName + "<br />");
                        //body.Append("Phát triển bởi Trung tâm Ứng dụng CNTT (CAIT)- ĐHQG Hà Nội<br />");
                        //body.Append("Webmaster: support@vnu.edu.vn");

                        bool result2 = await SendEmail.SendAsync(systemInfo.SMTPName, systemInfo.SMTPEmail, systemInfo.SMTPPassword, email, "Xác nhận khôi phục mật khẩu", body.ToString());
                        if (!result2)
                        {//Gửi email không thành công
                            forgetPassword.Status = 4;
                            int result1 = await _repository.GetRepository<ForgetPassword>().UpdateAsync(forgetPassword, account.Id);
                            TempData["Error"] = "Đã xảy ra lỗi khi thực hiện yêu cầu của bạn! (không gửi được email)";
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            TempData["Message"] = "Yêu cầu khôi phục mật khẩu của bạn đã được chấp nhận. Vui lòng kiểm tra email và làm theo hướng dẫn.";
                            return RedirectToAction("Index", "Login");
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Lỗi không xác định! Vui lòng thử lại!";
                        return RedirectToAction("Index", "Login");
                    }
                }
            }
            else
            {
                TempData["Error"] = "Lỗi không xác định! Vui lòng thử lại!";
                return RedirectToAction("Index", "Login");
            }
        }
        /// <summary>
        /// Xác nhận yêu cầu mật khẩu mới
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("~/xac-nhan-khoi-phuc-mat-khau/{code?}")]
        public async Task<ActionResult> ConfirmPassword(string code)
        {
            try
            {
                ViewBag.Message = "";
                var forgetPassword = await _repository.GetRepository<ForgetPassword>().ReadAsync(o => o.ActiveCode == code);
                if (forgetPassword == null)
                {
                    ViewBag.Message = "Yêu cầu của bạn không chính xác. Vui lòng kiểm tra lại!";
                    return View();
                }
                else
                {
                    if (forgetPassword.Status != 0)
                    {
                        ViewBag.Message = "Yêu cầu của bạn không chính xác. Vui lòng kiểm tra lại!";
                        return View();
                    }
                    DateTime requestTime = forgetPassword.RequestTime;
                    TimeSpan timespan = DateTime.Now - requestTime;
                    double hours = timespan.TotalHours;
                    if (hours > 24)
                    {
                        ViewBag.Message = "Yêu cầu khôi phục mật khẩu của bạn đã hết thời hạn!";
                        forgetPassword.Status = 3;
                        int result1 = await _repository.GetRepository<ForgetPassword>().UpdateAsync(forgetPassword, forgetPassword.AccountId);
                        return View();
                    }
                    var account = await _repository.GetRepository<Account>().ReadAsync(forgetPassword.AccountId);
                    account.Password = Base64Hepler.DecodeFrom64(forgetPassword.TemporaryPassword);
                    int result2 = await _repository.GetRepository<Account>().UpdateAsync(account, forgetPassword.AccountId);
                    if (result2 > 0)
                    {
                        forgetPassword.Status = 1;
                        forgetPassword.ActiveTime = DateTime.Now;
                        int result1 = await _repository.GetRepository<ForgetPassword>().UpdateAsync(forgetPassword, forgetPassword.AccountId);
                        ViewBag.Message = "Yêu cầu của bạn đã được xử lý thành công. Bạn đã có thể dùng mật khẩu mới để truy cập!";
                        //Có cần gửi email thông báo là đổi mật khẩu thành công hay không?
                        return View();
                    }
                    else
                    {
                        forgetPassword.Status = 2;
                        int result1 = await _repository.GetRepository<ForgetPassword>().UpdateAsync(forgetPassword, forgetPassword.AccountId);
                        ViewBag.Message = "Yêu cầu của bạn đã được xử lý không thành công. Vui lòng thử lại!";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi kích hoạt mật khẩu: " + ex.Message;
                return View();
            }
        }
        // khởi tạo data
        void KhoitaoTrangThai()
        {
            var tt = _repository.GetRepository<TrangThai>();
            for (int i = 1; i < 6; i++)
            {
                if (i == 1)
                {
                    var nitt = new TrangThai()
                    {
                        Ma = "0" + i.ToString(),
                        ThuTu = i,
                        Ten = "Chờ xét duyệt",
                        MauSacHienThi = "#f76221",
                        IdNguoiTao = 1,
                        LaTrangThaiBatDau = true,
                        LaTrangThaiKetThuc = false,
                        DuocSua = true,
                        DuocXoa = true,
                        KichHoat = true,
                        NgayTao = DateTime.Now
                    };
                    tt.Create(nitt, 1);
                }
                if (i == 2)
                {
                    var nitt = new TrangThai()
                    {
                        Ma = "0" + i.ToString(),
                        ThuTu = i,
                        Ten = "Chờ xử lý",
                        MauSacHienThi = "#2331ab",
                        LaTrangThaiKetThuc = false,
                        LaTrangThaiBatDau = false,
                        IdNguoiTao = 1,
                        DuocSua = true,
                        DuocXoa = true,
                        KichHoat = true,
                        NgayTao = DateTime.Now
                    };
                    tt.Create(nitt, 1);
                }
                if (i == 3)
                {
                    var nitt = new TrangThai()
                    {
                        Ma = "0" + i.ToString(),
                        ThuTu = i,
                        Ten = "Đã xử lý",
                        MauSacHienThi = "#22a800",
                        LaTrangThaiKetThuc = true,
                        LaTrangThaiBatDau = false,
                        IdNguoiTao = 1,
                        DuocSua = false,
                        DuocXoa = false,
                        KichHoat = true,
                        NgayTao = DateTime.Now
                    };
                    tt.Create(nitt, 1);
                }
                if (i == 5)
                {
                    var nitt = new TrangThai()
                    {
                        Ma = "0" + i.ToString(),
                        ThuTu = i,
                        Ten = "từ chối xử lý",
                        MauSacHienThi = "#706c6d",
                        LaTrangThaiKetThuc = true,
                        LaTrangThaiBatDau = false,
                        IdNguoiTao = 1,
                        DuocSua = true,
                        DuocXoa = true,
                        KichHoat = true,
                        NgayTao = DateTime.Now
                    };
                    tt.Create(nitt, 1);
                }
            }
        }
        // khởi tạo mục tin
        void InitMucTin()
        {
            for (int i = 0; i < 8; i++)
            {
                var Mt = new MucTin();
                if (i == 0)
                {
                    Mt.Code = "TrangChu";
                    Mt.Icon = "fa fa-home";
                    Mt.Name = "Trang chủ";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Dự án chung cư Iris Garden Mỹ Đình";
                }
                if (i == 1)
                {
                    Mt.Code = "ViTri";
                    Mt.Icon = "NoIcon";
                    Mt.Name = "Vị trí";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Vị trí đắc địa của chung cư Iris Garden Mỹ Đình";
                }
                if (i == 2)
                {
                    Mt.Code = "TienIch";
                    Mt.Icon = "NoIcon";
                    Mt.Name = "Tiện ích";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Hệ thống tiện ích đẳng cấp tại dự án Iris Garden Mỹ Đình";
                }
                if (i == 3)
                {
                    Mt.Code = "Gia";
                    Mt.Icon = "NoIcon";
                    Mt.Name = "Giá bán";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Giá bán chính thức chung cư Iris Garden Mỹ Đình";
                }
                if (i == 4)
                {
                    Mt.Code = "ChuDauTu";
                    Mt.Icon = "NoIcon";
                    Mt.Name = "Chủ đầu tư";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Chủ đầu tư dự án Iris Garden là Trần Hữu Dực";
                }
                if (i == 5)
                {
                    Mt.Code = "ChinhSach";
                    Mt.Icon = "NoIcon";
                    Mt.Name = "Chính sách";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Chính sách ";
                }
                if (i == 6)
                {
                    Mt.Code = "TinTuc";
                    Mt.Icon = "fa fa-new";
                    Mt.Name = "Tin tức";
                    Mt.NgayTao = DateTime.Now;
                    Mt.ThuTu = i;
                    Mt.TieuDe = "Tin tức dự án Iris Garden Mỹ Đình";
                }

                try
                {
                    _repository.GetRepository<MucTin>().Create(Mt, 1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("lỗi khi khởi tạo mục tin");
                }

            }
        }
        // khởi tạo khách hàng
        void InitKhachHang()
        {
            for (int i = 0; i < 5; i++)
            {
                var kh = new KhachHang()
                {
                    Email = "email00" + i.ToString() + "@gmail.com",
                    Name = "Name00" + i.ToString(),
                    NhuCau = "mk xin bảng giá và dánh ách căn hộ trống hiện tại",
                    PhoneNumber = "01627835923",
                    NgayTao = DateTime.Now,
                    TrangThaiKhachHang = TrangThaiKhachHangEnum.ChuaLienHe
                };
                try
                {
                    var rp = _repository.GetRepository<KhachHang>().Create(kh, 0);
                    if (rp > 0) Debug.WriteLine("khởi tạo thành công" + kh.Name);
                    else
                    {
                        Debug.WriteLine("Không khởi tạo được" + kh.Name);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }


    }
}