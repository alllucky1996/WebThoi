using Entities.Enums;
using Entities.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Helpers;
using Common.Paging;
using Entities.Models.SystemManage;
using Web.Areas.Management.Filters;
using Entities.Models;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("tai-khoan")]
    public class AccountController : BaseController
    {
        #region Danh sách tài khoản người dùng
        /// <summary>
        /// Danh sách tài khoản
        /// Dữ liệu được lấy bằng ajax theo hàm: GetAccountsJson 
        /// </summary>
        /// <returns></returns>
        [Route("danh-sach-tai-khoan", Name = "AccountIndex")]
        [ValidationPermission(Action = ActionEnum.Read, Module = ModuleEnum.Account)]
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region Tạo tài khoản người dùng mới
        /// <summary>
        /// Nhập tài khoản hệ thống mới
        /// </summary>
        /// <returns></returns>
        [Route("nhap-tai-khoan-moi", Name = "AccountCreate")]
        public ActionResult Create()
        {
            var dsDonVi = _repository.GetRepository<dmDonVi>().GetAll(o => o.CapDV == 1).ToList();
            ViewBag.IdDonVi = new SelectList(dsDonVi, "IdDV", "TenDV", "");
            return View();
        }
        /// <summary>
        /// Nhập tài khoản hệ thống mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("nhap-tai-khoan-moi")]
        [ValidationPermission(Action = ActionEnum.Create, Module = ModuleEnum.Account)]
        public async Task<ActionResult> Create(Account model, HttpPostedFileBase file)
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
                    string password = StringHelper.StringToMd5(StringHelper.KillChars(model.Password)).ToLower();
                    Account account = new Account();
                    //account.IsManageAccount = false;
                    //account.IsNormalAccount = false;
                    account.Email = email;
                    account.FullName = StringHelper.KillChars(model.FullName);
                    //
                    account.IsManageAccount = false;
                    account.CapQuanLy = model.CapQuanLy;
                    //account.Code = StringHelper.KillChars(model.Code);
                    account.PhoneNumber = StringHelper.KillChars(model.PhoneNumber);
                    //account.IsExpertsAccount = model.IsExpertsAccount;
                    if (!string.IsNullOrEmpty(photo))
                        account.ProfilePicture = photo;// StringHelper.KillChars(model.ProfilePicture); ;
                    account.CreateDate = DateTime.Now;
                    account.IdDonVi = model.IdDonVi;

                    account.Password = StringHelper.StringToMd5(password);
                    int result = await _repository.GetRepository<Account>().CreateAsync(account, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Nhập tài khoản mới thành công!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nhập tài khoản mới không thành công! Vui lòng kiểm tra và thử lại!");
                        var dsDonVi = _repository.GetRepository<dmDonVi>().GetAll(o => o.CapDV == 1).ToList();
                        ViewBag.IdDonVi = new SelectList(dsDonVi, "IdDV", "TenDV", "");
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập chính xác các thông tin!");
                var dsDonVi = _repository.GetRepository<dmDonVi>().GetAll(o => o.CapDV == 1).ToList();
                ViewBag.IdDonVi = new SelectList(dsDonVi, "IdDV", "TenDV", "");
                return View(model);
            }
        }
        #endregion
        #region Cập nhật thông tin người dùng
        /// <summary>
        /// Chi tiết thông tin tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidationPermission(Action = ActionEnum.Read, Module = ModuleEnum.Account)]
        [Route("cap-nhat-tai-khoan/{id?}", Name = "AccountRead")]
        public async Task<ActionResult> Read(long id)
        {
            ViewBag.Roles = await _repository.GetRepository<Role>().GetAllAsync();
            ViewBag.AccountRoles = await _repository.GetRepository<AccountRole>().GetAllAsync(o => o.AccountId == id);
            ViewBag.AccountUngDungs = await _repository.GetRepository<AccountUngDung>().GetAllAsync(o => o.AccountId == id);

            var dsDonVi = _repository.GetRepository<dmDonVi>().GetAll(o => o.CapDV == 1).ToList();
            ViewBag.IdDonVi = new SelectList(dsDonVi, "Id", "Name", "");

            Account account = await _repository.GetRepository<Account>().ReadAsync(id);
            return View(account);
        }
        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("cap-nhat-tai-khoan/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Account)]
        public async Task<ActionResult> Read(long id, AccountUpdatingViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
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
                    bool result = await _repository.GetRepository<Account>().AnyAsync(o => o.Email == email && o.Id != id);
                    if (result)
                        return Json(new { success = false, message = "Địa chi email đã được sử dụng. Vui lòng nhập địa chỉ email khác!" }, JsonRequestBehavior.AllowGet);
                    Account account = await _repository.GetRepository<Account>().ReadAsync(id);
                    if (account == null)
                        return Json(new { success = false, message = "Không tìm thấy tài khoản người dùng!" }, JsonRequestBehavior.AllowGet);
                    //Cập nhật thông tin
                    string dob = StringHelper.KillChars(model.DateOfBirth);
                    if (!string.IsNullOrEmpty(dob))
                    {
                        try
                        {
                            DateTime date = DateTime.ParseExact(dob, "dd/MM/yyyy", null);
                            account.DateOfBirth = date;
                        }
                        catch
                        {
                            account.DateOfBirth = null;
                        }
                    }
                    account.FullName = StringHelper.KillChars(model.FullName);
                    //account.Code = StringHelper.KillChars(model.Code);
                    account.Email = email;
                    account.PhoneNumber = StringHelper.KillChars(model.PhoneNumber);
                    //account.IsExpertsAccount = model.IsExpertsAccount;
                    account.Sex = model.Sex;
                    account.Address = StringHelper.KillChars(model.Address);
                    account.IdDonVi = model.IdDonVi;
                    if (!string.IsNullOrEmpty(photo))
                        account.ProfilePicture = photo;//StringHelper.KillChars(model.ProfilePicture); ;
                    int updateResult = await _repository.GetRepository<Account>().UpdateAsync(account, AccountId);
                    if (updateResult > 0)
                    {
                        if (id == AccountId)
                        {
                            Session["Email"] = account.Email;
                            Session["AccountId"] = account.Id;
                            Session["AccountName"] = account.FullName;
                            Session["ProfilePicture"] = account.ProfilePicture;
                            Session["IsExpertsAccount"] = account.IsExpertsAccount;
                            Session["IsManageAccount"] = account.IsManageAccount;
                            Session[SessionEnum.AccountDv] = account.IdDonVi.ToString();
                        }
                        return Json(new { success = true, message = "Cập nhật thông tin tài khoản thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { success = false, message = "Cập nhật thông tin tài khoản không thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập chính xác các thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Đổi mật khẩu của người dùng hiện tại đang đăng nhập
        [Route("doi-mat-khau", Name = "AccountChangePassword")]
        public ActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }
        //Đổi mật khẩu của người đang đăng nhập
        [Route("doi-mat-khau", Name = "AccountChangePasswordPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long accountId = Convert.ToInt64(Session["AccountId"].ToString());
                    Account account = await _repository.GetRepository<Account>().ReadAsync(accountId);
                    if (account == null)
                        return Json(new { success = false, message = "Không tìm thấy tài khoản người dùng!" }, JsonRequestBehavior.AllowGet);
                    string password = StringHelper.StringToMd5(StringHelper.StringToMd5(StringHelper.KillChars(model.OldPassword)).ToLower());
                    string newPassword = StringHelper.StringToMd5(StringHelper.StringToMd5(StringHelper.KillChars(model.NewPassword)).ToLower());
                    //Kiểm tra mật khẩu cũ
                    if (password == account.Password)
                    {
                        account.Password = newPassword;
                        int result = await _repository.GetRepository<Account>().UpdateAsync(account, AccountId);
                        if (result > 0)
                            return Json(new { success = true, message = "Đổi mật khẩu thành công!" }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { success = false, message = "Đổi mật khẩu không thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mật khẩu hiện tại không chính xác, vui lòng kiểm tra lại!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập chính xác các thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Đổi mật khẩu cho người dùng
        /// <summary>
        /// Đổi mật khẩu cho thành viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("doi-mat-khau-tai-khoan/{id?}", Name = "AccountChangeMemberPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Account)]
        public async Task<ActionResult> ChangeMemberPassword(long id, ChangeMemberPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account account = await _repository.GetRepository<Account>().ReadAsync(id);
                    if (account == null)
                        return Json(new { success = false, message = "Không tìm thấy tài khoản người dùng!" }, JsonRequestBehavior.AllowGet);
                    string password = StringHelper.StringToMd5(StringHelper.StringToMd5(StringHelper.KillChars(model.ConfirmPassword)).ToLower());
                    string newPassword = StringHelper.StringToMd5(StringHelper.StringToMd5(StringHelper.KillChars(model.NewPassword)).ToLower());
                    //Kiểm tra mật khẩu có đồng nhất hay không
                    if (password == newPassword)
                    {
                        account.Password = newPassword;
                        int result = await _repository.GetRepository<Account>().UpdateAsync(account, AccountId);
                        if (result > 0)
                            return Json(new { success = true, message = "Đổi mật khẩu thành công!" }, JsonRequestBehavior.AllowGet);
                        else
                            return Json(new { success = false, message = "Đổi mật khẩu không thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mật khẩu không khớp, vui lòng kiểm tra lại!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập chính xác các thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Xóa 1 tài khoản
        /// <summary>
        /// Xóa tài khoản người dùng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidationPermission(Action = ActionEnum.Delete, Module = ModuleEnum.Account)]
        [Route("xoa-tai-khoan/{id?}", Name = "AccountDelete")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                if (id == AccountId || id == 1)
                {
                    TempData["Error"] = "Xóa tài khoản không thành công!";
                    return RedirectToAction("Index");
                }
                Account account = await _repository.GetRepository<Account>().ReadAsync(id);
                if (account != null)
                {
                    int result1 = await _repository.GetRepository<AccountRole>().DeleteAsync(o => o.AccountId == id, AccountId);
                   
                    int result = await _repository.GetRepository<Account>().DeleteAsync(account, AccountId);

                    if (result > 0)
                        TempData["Success"] = "Xóa tài khoản thành công!";
                    else
                        TempData["Error"] = "Xóa tài khoản không thành công!";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Xóa tài khoản không thành công! Lỗi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        #endregion
        #region Xóa nhiều tài khoản người dùng
        /// <summary>
        /// Xóa tài khoản người dùng
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [ValidationPermission(Action = ActionEnum.Delete, Module = ModuleEnum.Account)]
        [HttpPost]
        [Route("xoa-nhieu-tai-khoan/{ids?}", Name = "AccountDeleteAccounts")]
        public async Task<ActionResult> DeleteAccounts(string ids)
        {
            try
            {
                byte succeed = 0;
                string[] accountIds = Regex.Split(ids, ",");
                if (accountIds != null && accountIds.Any())
                    foreach (var item in accountIds)
                    {
                        long accountId = 0;
                        long.TryParse(item, out accountId);
                        bool result = await DeleteAccount(accountId);
                        if (result)
                            succeed++;
                    }
                return Json(new { success = true, message = string.Format(@"Đã xóa thành công {0} tài khoản.", succeed) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Không xóa được tài khoản. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Xóa tài khoản người dùng
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private async Task<bool> DeleteAccount(long accountId)
        {
            if (accountId == AccountId || accountId == 1)
                return false;
            var account = await _repository.GetRepository<Account>().ReadAsync(accountId);
            if (account == null)
                return false;

            //Xóa phân quyền
            int result1 = await _repository.GetRepository<AccountRole>().DeleteAsync(o => o.AccountId == accountId, AccountId);

            int result = await _repository.GetRepository<Account>().DeleteAsync(account, AccountId);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Phân quyền (phân vai trò) cho người dùng
        /// <summary>
        /// Phân quyền sử dụng chức năng
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Account)]
        [Route("phan-quyen-cho-tai-khoan", Name = "AccountMappingRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AccountMappingRole(long accountId, string roles)
        {
            try
            {
                string[] rolesChecked = Regex.Split(roles, ",");
                var accountRoles = rolesChecked.Where(o => !string.IsNullOrWhiteSpace(o)).Select(o => new AccountRole()
                    {
                        RoleId = Convert.ToInt64(o),
                        AccountId = accountId
                    }).ToList();
                try
                {
                    if (accountRoles.Any())
                    {
                        var accountRolesMapped = await _repository.GetRepository<AccountRole>().GetAllAsync(o => o.AccountId == accountId);
                        var toMap = accountRoles.Where(p => !accountRolesMapped.Any(p2 => p2.RoleId == p.RoleId));
                        var toUnMap = accountRolesMapped.Where(p => !accountRoles.Any(p2 => p2.RoleId == p.RoleId));
                        int result2 = await _repository.GetRepository<AccountRole>().DeleteAsync(toUnMap, AccountId);
                        int result = await _repository.GetRepository<AccountRole>().CreateAsync(toMap, AccountId);
                    }
                    else
                    {
                        int result = await _repository.GetRepository<AccountRole>().DeleteAsync(o => o.AccountId == accountId, AccountId);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Phân quyền không thành công. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
                var accountRoles2 = _repository.GetRepository<AccountRole>().GetAll(o => o.AccountId == AccountId).ToList();
                var moduleRoles = _repository.GetRepository<ModuleRole>().GetAll().ToList();
                CacheFactory _cacheFactory = new CacheFactory();
                _cacheFactory.SaveCache("AccountRoles_" + AccountId, accountRoles2);
                _cacheFactory.SaveCache("ModuleRoles", moduleRoles);

                return Json(new { success = true, message = "Đã phân quyền thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Phân quyền không thành công. Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Lấy danh sách tài khoản người dùng, trả về JSON
        [Route("danh-sach-tai-khoan-json/{status?}", Name = "AccountGetAccountsJson")]
        public ActionResult GetAccountsJson(byte status)
        {
            string drawReturn = "1";
            int skip = 0;
            int take = 10;

            string start = Request.Params["start"];//Đang hiển thị từ bản ghi thứ mấy
            string length = Request.Params["length"];//Số bản ghi mỗi trang
            string draw = Request.Params["draw"];//Số lần request bằng ajax (hình như chống cache)
            string key = Request.Params["search[value]"];//Ô tìm kiếm            
            string orderDir = Request.Params["order[0][dir]"];//Trạng thái sắp xếp xuôi hay ngược: asc/desc
            orderDir = string.IsNullOrEmpty(orderDir) ? "asc" : orderDir;
            string orderColumn = Request.Params["order[0][column]"];//Cột nào đang được sắp xếp (cột thứ mấy trong html table)
            orderColumn = string.IsNullOrEmpty(orderColumn) ? "1" : orderColumn;
            string orderKey = Request.Params["columns[" + orderColumn + "][data]"];//Lấy tên của cột đang được sắp xếp
            orderKey = string.IsNullOrEmpty(orderKey) ? "CreateDate" : orderKey;

            if (!string.IsNullOrEmpty(start))
                skip = Convert.ToInt16(start);
            if (!string.IsNullOrEmpty(length))
                take = Convert.ToInt16(length);
            if (!string.IsNullOrEmpty(draw))
                drawReturn = draw;
            string objectStatus = Request.Params["objectStatus"];//Lọc trạng thái 
            if (!string.IsNullOrEmpty(objectStatus))
                byte.TryParse(objectStatus.ToString(), out status);
            Paging paging = new Paging()
            {
                TotalRecord = 0,
                Skip = skip,
                Take = take,
                OrderDirection = orderDir
            };
            var accounts = _repository.GetRepository<Account>().GetAll(ref paging, orderKey, o => (
               key == null ||
               key == "" ||
               o.Email.Contains(key) ||
               o.FullName.Contains(key) ||
               o.PhoneNumber.Contains(key))
               ).ToList();
            //if (status == 1)
            //{
            //    accounts = accounts.Where(o => o.Profile == null).ToList();
            //}
            //if (status == 2)
            //{
            //    accounts = accounts.Where(o => o.Profile != null).ToList();
            //}

            return Json(new
            {
                draw = drawReturn,
                recordsTotal = paging.TotalRecord,
                recordsFiltered = paging.TotalRecord,
                data = accounts.Select(o => new
                {
                    o.Id,
                    o.FullName,
                    o.Email,
                    o.PhoneNumber,
                    CreateDate = o.CreateDate.ToString("dd/MM/yyyy")
                })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Lấy danh sách gợi ý, trả về JSON
        [Route("danh-sach-recom-json", Name = "AccountGetReComJson")]
        public ActionResult GetRecomsJson(string reg)
        {

            var exist = _repository.GetRepository<Account>().GetAll(o => o.Email.EndsWith("@vnu.edu.vn")).Select(o => o.Email).ToList();
            return Json(new
            {
                data = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        

    }
}