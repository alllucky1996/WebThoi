using Common.Helpers;
using Entities.Enums;
using Entities.Models;
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
    public class TrangThaiYeuCauController : BaseController
    {
		public const string CName="TrangThaiBaiViet";
        public const ModuleEnum CModule = ModuleEnum.TrangThaiBaiViet;
        public const string CRoute = "trang-thai-bai-viet";
        public const string CText = "Trạng thái bài viết";

        public IGenericRepository<TrangThai> GetRespository()
        {
            return _repository.GetRepository<TrangThai>();
        }
        public static TrangThai NewObject()
        {
            return new TrangThai();
        }

        public bool CanDelete(TrangThai deleteItem)
        {
            //if (deleteItem.YeuCaus != null && deleteItem.YeuCaus.Any())
            //     return false;
            return true;
        } 


        [Route("danh-muc-"+CRoute, Name = CName+"_Index")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> Index()
        {
            var list = await GetRespository().GetAllAsync();
            ViewBag.Title = "Danh mục" + CText;
            ViewBag.CanDelete = RoleHelper.CheckPermission(CModule, ActionEnum.Delete);
            ViewBag.CanCreate = RoleHelper.CheckPermission(CModule, ActionEnum.Create);
            ViewBag.CanUpdate = RoleHelper.CheckPermission(CModule, ActionEnum.Update);
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View(list.OrderBy(o => o.ThuTu));
        }

        [Route("nhap" + CRoute, Name = CName + "_Create")]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm mới "+ CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View();
        }
        [Route("nhap"+CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public async Task<ActionResult> Create(TrangThai model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Kiểm tra trùng mã
                    string ma = StringHelper.KillChars(model.Ma);
                    var any = await GetRespository().AnyAsync(o => o.Ma == ma);
                    if (any)
                    {
                        ViewBag.Error = "Mã này đã được sử dụng! Vui lòng nhập mã khác!";
                        return View(model);
                    }
                    //Nhập trạng thái bài viết
                    var newItem = NewObject();
                    newItem.Ma = StringHelper.KillChars(model.Ma);
                    newItem.Ten = StringHelper.KillChars(model.Ten);
                    newItem.ThuTu = model.ThuTu;
                    newItem.MauSacHienThi = StringHelper.KillChars(model.MauSacHienThi);
                    newItem.LaTrangThaiBatDau = model.LaTrangThaiBatDau;
                    newItem.LaTrangThaiKetThuc = model.LaTrangThaiKetThuc;
                    newItem.IdNguoiTao = AccountId;
                    newItem.NgayTao = DateTime.Now;
                    newItem.KichHoat = model.KichHoat;
                    newItem.DuocSua = model.DuocSua;
                    newItem.DuocXoa = model.DuocXoa;
                    int result = await GetRespository().CreateAsync(newItem, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Thêm mới thành công "+CText;
                        return RedirectToRoute(CName+"_Index");
                    }
                    else
                    {
                        ViewBag.Error = "Không thêm được "+CText;
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
        [Route("cap-nhat-"+CRoute+"/{ma}", Name = CName+"_Update")]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(string ma)
        {
            var editingItem = await GetRespository().ReadByKeyAsync(ma);
            if (editingItem == null)
            {
                TempData["Error"] = "Không tìm thấy "+CText;
                return RedirectToRoute(CName+"_Index");
            }
            ViewBag.Title = "Sửa " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View(editingItem);
        }
        [Route("cap-nhat-"+CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(string ma, TrangThai model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Cập nhật trạng thái bài viết
                    var updateItem = await GetRespository().ReadByKeyAsync(ma);
                    if (updateItem == null)
                    {
                        TempData["Error"] = "Không tìm thấy "+CText;
                        return RedirectToRoute(CName+"_Index");
                    }
                    //Không cho sửa mã, nếu cho sửa phải kiểm tra trùng
                    //deleteItem.Ma = StringHelper.KillChars(model.Ma);
                    updateItem.Ten = StringHelper.KillChars(model.Ten);
                    updateItem.ThuTu = model.ThuTu;
                    updateItem.MauSacHienThi = StringHelper.KillChars(model.MauSacHienThi);
                    updateItem.LaTrangThaiBatDau = model.LaTrangThaiBatDau;
                    updateItem.LaTrangThaiKetThuc = model.LaTrangThaiKetThuc;
                    updateItem.KichHoat = model.KichHoat;
                    updateItem.DuocSua = model.DuocSua;
                    updateItem.DuocXoa = model.DuocXoa;
                    int result = await GetRespository().UpdateAsync(updateItem, AccountId);
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
        [Route("Xoa-"+CRoute+"/{code?}", Name = CName+"_Delete")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Delete, Module = CModule)]
        public async Task<JsonResult> Delete(string code)
        {
            try
            {
                var deleteItem = await GetRespository().ReadByKeyAsync(code);
                if (deleteItem != null)
                {
                    if(!CanDelete(deleteItem))return Json(new { success = false, message = CText+" đang được sử dụng!" });
                    int result = await GetRespository().DeleteAsync(deleteItem, AccountId);
                    if (result > 0)
                    {
                        return Json(new { success = true, message = "Xóa thành công!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Lỗi: Không xóa được bản ghi này!" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Lỗi: Không tìm thấy đối tượng!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}