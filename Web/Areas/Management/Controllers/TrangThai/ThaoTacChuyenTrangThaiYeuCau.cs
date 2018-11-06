using Common.Helpers;
using Entities.Enums;
using Entities.Models;
using Entities.Models.ThongKe;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Management.Filters;
using Web.Areas.Management.Helpers;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class ThaoTacChuyenTrangThaiYeuCauController : BaseController
    {
        public const string CName = "ThaoTacChuyenTrangThai";
        public const string CRoute = "thao-tac-chuyen-trang-thai";
        public const string CText = "Thao tác chuyển trạng thái";
        public const ModuleEnum CModule = ModuleEnum.ThaoTacChuyenTrangThai;

        public IGenericRepository<ChuyenTrangThaiYeuCau> GetRespository()
        {
            return _repository.GetRepository<ChuyenTrangThaiYeuCau>();
        }
        public IGenericRepository<TrangThai> GetSubRespository()
        {
            return _repository.GetRepository<TrangThai>();
        }

        public static ChuyenTrangThaiYeuCau NewObject()
        {
            return new ChuyenTrangThaiYeuCau();
        }

        public bool CanDelete(ChuyenTrangThaiYeuCau deleteItem)
        {
            return true;
        } 

        [Route("quan-ly-" + CRoute, Name = CName+"_Index")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> Index()
        {
            var list = await GetRespository().GetAllAsync();
            ViewBag.Title = "Danh mục " + CText;
            ViewBag.CanDelete = RoleHelper.CheckPermission(CModule, ActionEnum.Delete);
            ViewBag.CanCreate = RoleHelper.CheckPermission(CModule, ActionEnum.Create);
            ViewBag.CanUpdate = RoleHelper.CheckPermission(CModule, ActionEnum.Update);
            ViewBag.CName = CName;
            ViewBag.CText = CText;

            return View(list.OrderBy(o => o.MaTrangThaiNguon));
        }

        [Route("nhap-"+CRoute, Name = CName+"_Create")]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public async Task<ActionResult> Create()
        {
            //Lay danh sach vao dropdownlist
            var list = await _repository.GetRepository<TrangThai>().GetAllAsync();
     
            ViewBag.MaTrangThaiNguon = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", "");
            ViewBag.MaTrangThaiDich = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", "");
            //End comment
            ViewBag.Title = "Thêm mới " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View();
        }
        [Route("nhap-"+CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public async Task<ActionResult> Create(ChuyenTrangThaiYeuCau model)
        {
            var list = await GetSubRespository().GetAllAsync();
            ViewBag.MaTrangThaiNguon = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", model.MaTrangThaiNguon);
            ViewBag.MaTrangThaiDich = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", model.MaTrangThaiDich);
            if (ModelState.IsValid)
            {
                try
                {
                    //Nhập thao tác chuyển trạng thái
                    string nguon = StringHelper.KillChars(model.MaTrangThaiNguon);
                    string dich = StringHelper.KillChars(model.MaTrangThaiDich);
                    var any = await GetSubRespository().AnyAsync(o => o.Ma == nguon);
                    if (!any)
                    {
                        ViewBag.Error = "Không tìm thấy trạng thái!";
                        return View(model);
                    }
                    var any2 = await GetSubRespository().AnyAsync(o => o.Ma == dich);
                    if (!any2)
                    {
                        ViewBag.Error = "Không tìm thấy trạng thái!";
                        return View(model);
                    }
                    var item = NewObject();
                    item.TenThaoTac = StringHelper.KillChars(model.TenThaoTac);
                    item.MaTrangThaiNguon = nguon;
                    item.MaTrangThaiDich = dich;
                    item.MauSacHienThi = StringHelper.KillChars(model.MauSacHienThi);

                    item.IdNguoiTao = AccountId;
                    item.NgayTao = DateTime.Now;
                    item.KichHoat = model.KichHoat;

                    item.CoXacNhan = model.CoXacNhan;
                    item.NoiDungXacNhan = StringHelper.KillChars(model.NoiDungXacNhan);
                    item.RouteXacNhan = StringHelper.KillChars(model.RouteXacNhan);

                    ViewBag.Title = "Sửa " + CText;
                    ViewBag.CName = CName;
                    ViewBag.CText = CText;
                    int result = await GetRespository().CreateAsync(item, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Thêm mới thành công!";
                        return RedirectToRoute(CName+"_Index");
                    }
                    else
                    {
                        ViewBag.Error = "Không thêm được!";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                    return View(model);
                }
            }
            else
            {
                ViewBag.Error = "Vui lòng nhập chính xác các thông tin!";
                return View(model);
            }
        }
        [Route("cap-nhat-"+CRoute+"/{id?}", Name = CName+"_Update")]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(long id)
        {
            var item = await GetRespository().ReadAsync(id);
            if (item == null)
            {
                TempData["Error"] = "Không tìm thấy thao tác chuyển trạng thái!";
                return RedirectToRoute(CName+"_Index");
            }
            var list = await GetSubRespository().GetAllAsync();
            ViewBag.MaTrangThaiNguon = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", item.MaTrangThaiNguon);
            ViewBag.MaTrangThaiDich = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", item.MaTrangThaiDich);
            ViewBag.Title = "Sửa " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;

            return View(item);
        }
        [Route("cap-nhat-"+CRoute+"/{id?}")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(long id, ChuyenTrangThaiYeuCau model)
        {
            var list = await GetSubRespository().GetAllAsync();
            ViewBag.MaTrangThaiNguon = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", model.MaTrangThaiNguon);
            ViewBag.MaTrangThaiDich = new SelectList(list.OrderBy(o => o.ThuTu), "Ma", "Ten", model.MaTrangThaiDich);
            if (ModelState.IsValid)
            {
                try
                {
                    //Cập nhật thao tác chuyển trạng thái
                    var item = await GetRespository().ReadAsync(id);
                    if (item == null)
                    {
                        TempData["Error"] = "Không tìm thấy thao tác!";
                        return RedirectToRoute(CName+"_Index");
                    }
                    string nguon = StringHelper.KillChars(model.MaTrangThaiNguon);
                    string dich = StringHelper.KillChars(model.MaTrangThaiDich);
                    var any = await GetSubRespository().AnyAsync(o => o.Ma == nguon);
                    if (!any)
                    {
                        ViewBag.Error = "Không tìm thấy trạng thái!";
                        return View(model);
                    }
                    var any2 = await GetSubRespository().AnyAsync(o => o.Ma == dich);
                    if (!any2)
                    {
                        ViewBag.Error = "Không tìm thấy trạng thái!";
                        return View(model);
                    }
                    item.TenThaoTac = StringHelper.KillChars(model.TenThaoTac);
                    item.MaTrangThaiNguon = nguon;
                    item.MaTrangThaiDich = dich;
                    item.MauSacHienThi = StringHelper.KillChars(model.MauSacHienThi);
                    item.KichHoat = model.KichHoat;

                    item.CoXacNhan = model.CoXacNhan;
                    item.NoiDungXacNhan = StringHelper.KillChars(model.NoiDungXacNhan);
                    item.RouteXacNhan = StringHelper.KillChars(model.RouteXacNhan);

                    ViewBag.Title = "Sửa " + CText;
                    ViewBag.CName = CName;
                    ViewBag.CText = CText;
                    int result = await GetRespository().UpdateAsync(item, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Ghi nhận thành công!";
                        return RedirectToRoute(CName+"_Index");
                    }
                    else
                    {
                        ViewBag.Error = "Không ghi nhận được!";
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                    return View(model);
                }
            }
            else
            {
                ViewBag.Error = "Vui lòng nhập chính xác các thông tin!";
                return View(model);
            }
        }
        [Route("xoa-"+CRoute+"/{id?}", Name = CName+"_Delete")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Delete, Module = CModule)]
        public async Task<JsonResult> Delete(long id)
        {
            try
            {
                var item = await GetRespository().ReadAsync(id);
                if (item != null)
                {
                    int result = await GetRespository().DeleteAsync(item, AccountId);
                    if (result > 0)
                    {
                        return Json(new { success = true, message = "Đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Lỗi: Không xóa được!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Lỗi: Không tìm thấy thao tác chuyển trạng thái!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}