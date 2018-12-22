using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Helpers;
using Entities.Models.SystemManage;
using Web.Areas.Management.Filters;
using Web.Helpers;
namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("nhom-quyen")]
    public class RoleController : BaseController
    {
        [Route("quan-ly-nhom-quyen", Name = "RoleIndex")]
        [ValidationPermission(Action = ActionEnum.Read, Module = ModuleEnum.Role)]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Role> roles = await _repository.GetRepository<Role>().GetAllAsync();
            return View(roles);
        }
        [Route("nhap-nhom-quyen", Name = "RoleCreate")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Create, Module = ModuleEnum.Role)]
        public async Task<JsonResult> Create(Role model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Name = StringHelper.KillChars(model.Name);
                    model.Description = StringHelper.KillChars(model.Description);
                    int result = await _repository.GetRepository<Role>().CreateAsync(model, AccountId);
                    if (result > 0)
                    {
                        return Json(new { success = true, id = model.Id }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không nhập được nhóm quyền!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Lỗi:" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ và chính xác các thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("cap-nhat-nhom-quyen", Name = "RoleUpdate")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Role)]
        public async Task<JsonResult> Update(Role model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Role role = await _repository.GetRepository<Role>().ReadAsync(model.Id);
                    if (role == null)
                        return Json(new { success = false, message = "Không tìm thấy nhóm quyền!" }, JsonRequestBehavior.AllowGet);
                    role.Name = StringHelper.KillChars(model.Name);
                    role.Description = StringHelper.KillChars(model.Description);
                    int result = await _repository.GetRepository<Role>().UpdateAsync(role, AccountId);
                    if (result > 0)
                    {
                        return Json(new { success = true, id = model.Id }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Không cập nhật được nhóm quyền!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Lỗi:" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ và chính xác các thông tin!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("xoa-nhom-quyen/{id?}", Name = "RoleDelete")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Delete, Module = ModuleEnum.Role)]
        public async Task<JsonResult> Delete(long id)
        {
            try
            {
                Role role = await _repository.GetRepository<Role>().ReadAsync(id);
                if (role != null)
                {
                    //Nếu role đang được phân quyền thì cho xóa hay báo lỗi???
                    if (role.AccountRoles != null && role.AccountRoles.Any())
                    {
                        //Cho xóa thì xóa phân quyền trước
                        await _repository.GetRepository<AccountRole>().DeleteAsync(o => o.RoleId == id, AccountId);
                        //Không cho xóa thì báo lỗi
                        //return Json(new { success = false, message = "Lỗi: Nhóm quyền đang được sử dụng!" });
                    }
                    //Nếu role đã đươc khai báo module thì cho xóa hay báo lỗi???
                    if (role.RoleModules != null && role.RoleModules.Any())
                    {
                        //Cho xóa thì xóa module trước
                        await _repository.GetRepository<ModuleRole>().DeleteAsync(o => o.RoleId == id, AccountId);
                        //Không cho xóa thì báo lỗi
                        //return Json(new { success = false, message = "Lỗi: Nhóm quyền đang được sử dụng!" });
                    }
                    int result = await _repository.GetRepository<Role>().DeleteAsync(role, AccountId);
                    if (result > 0)
                    {
                        var accountRoles = _repository.GetRepository<AccountRole>().GetAll(o => o.AccountId == AccountId).ToList();
                        var moduleRoles = _repository.GetRepository<ModuleRole>().GetAll().ToList();
                        CacheFactory _cacheFactory = new CacheFactory();
                        _cacheFactory.SaveCache("AccountRoles_" + AccountId, accountRoles);
                        _cacheFactory.SaveCache("ModuleRoles", moduleRoles);
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Lỗi: Không xóa được nhóm quyền!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Lỗi: Không tìm thấy nhóm quyền!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("cap-nhap-chuc-nang-cho-nhom-quyen/{id?}", Name = "RoleModuleRole")]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Role)]
        public async Task<ActionResult> ModuleRole(long id)
        {
            ViewBag.RoleId = id;
            var role = await _repository.GetRepository<Role>().ReadAsync(id);
            ViewBag.RoleName = role.Name;
            ViewBag.ModuleRoles = await _repository.GetRepository<ModuleRole>().GetAllAsync();
            ViewBag.RoleContent = role;
            return View();
        }
        [Route("cap-nhap-chuc-nang-cho-nhom-quyen/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = ModuleEnum.Role)]
        public async Task<ActionResult> ModuleRole()
        {
            var noduleCodes = ModuleEnum.Account.ToSelectListEnumRaw();
            long roleId = Convert.ToInt64(Request.Params["roleId"]);

            var roleCt = _repository.GetRepository<Role>().Read(roleId);

            roleCt.IsLevel1 = (bool)("on".Equals(Request.Params["IsLevel1"]) ? true : false);
            roleCt.IsLevel2 = (bool)("on".Equals(Request.Params["IsLevel2"]) ? true : false);
            roleCt.IsLevel3 = (bool)("on".Equals(Request.Params["IsLevel3"]) ? true : false);

            _repository.GetRepository<Role>().Update(roleCt, AccountId);

            var moduleRoles = noduleCodes.Select(item => new ModuleRole()
            {
                RoleId = roleId,
                Read = (byte?)("on".Equals(Request.Params[item.Value + "_Read"]) ? 1 : 0),
                Create = (byte?)("on".Equals(Request.Params[item.Value + "_Create"]) ? 1 : 0),
                Update = (byte?)("on".Equals(Request.Params[item.Value + "_Update"]) ? 1 : 0),
                Delete = (byte?)("on".Equals(Request.Params[item.Value + "_Delete"]) ? 1 : 0),
                Verify = (byte?)("on".Equals(Request.Params[item.Value + "_Verify"]) ? 1 : 0),
                //Publish = (byte?)("on".Equals(Request.Params[item.Value + "_Publish"]) ? 1 : 0),
                ModuleCode = item.Value
            }).ToList();
            if (moduleRoles != null && moduleRoles.Any())
            {
                foreach (var item in moduleRoles)
                {
                    ModuleRole moduleRole = await _repository.GetRepository<ModuleRole>().ReadAsync(o => o.RoleId == roleId && o.ModuleCode == item.ModuleCode);
                    if (moduleRole != null)
                    {
                        moduleRole.Read = item.Read;
                        moduleRole.Create = item.Create;
                        moduleRole.Update = item.Update;
                        moduleRole.Delete = item.Delete;
                        moduleRole.Verify = item.Verify;
                        //moduleRole.Publish = item.Publish;
                        await _repository.GetRepository<ModuleRole>().UpdateAsync(moduleRole, AccountId);
                    }
                    else
                    {
                        await _repository.GetRepository<ModuleRole>().CreateAsync(item, AccountId);
                    }
                }
            }
            var accountRoles = _repository.GetRepository<AccountRole>().GetAll(o => o.AccountId == AccountId).ToList();
            var moduleRoles1 = _repository.GetRepository<ModuleRole>().GetAll().ToList();
            CacheFactory _cacheFactory = new CacheFactory();
            _cacheFactory.SaveCache("AccountRoles_" + AccountId, accountRoles);
            _cacheFactory.SaveCache("ModuleRoles", moduleRoles1);
            TempData["Success"] = "Đã ghi nhận thành công!";
            return RedirectToRoute("RoleModuleRole", new { id = roleId });
        }
    }
}