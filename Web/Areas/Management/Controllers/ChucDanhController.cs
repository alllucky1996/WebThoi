using Common.Helpers;
using Entities.Enums;
using Entities.Models;
using Entities.Models.SystemManage;
using Entities.ViewModels;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Management.Filters;
using Web.Areas.Management.Helpers;

namespace Web.Areas.Management.Controllers
{
    /// <summary>
    /// Phòng ban là cấp 2 
    /// Ngay bên dưới công ty
    /// </summary>
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class ChucDanhController : BaseController
    {

        public const string CName = "ChucDanh";
        public const ModuleEnum CModule = ModuleEnum.DonVi;
        public const string CRoute = "chuc-danh";
        public const string CText = " chức danh";
        void BaseView()
        {
            ViewBag.Title = "Danh mục" + CText;
            ViewBag.CName = CName;
            ViewBag.CRoute = CRoute;
            ViewBag.CText = CText;
        }

        public IGenericRepository<CapQuanLy> GetRespository()
        {
            return _repository.GetRepository<CapQuanLy>();
        }
        public static CapQuanLy NewObject()
        {
            return new CapQuanLy();
        }

        public bool CanDelete(CapQuanLy deleteItem)
        {
            //if (deleteItem.YeuCaus != null && deleteItem.YeuCaus.Any())
            //     return false;
            return true;
        }


        [Route("danh-muc-" + CRoute, Name = CName + "_Index")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> Index(long? DonVi)
        {
            Expression<Func<CapQuanLy, bool>> filterExpression;
            BaseView();
            // điều kiện lọc ở dropdow
            if(DonVi == null)
            {
                var listDonVi = await _repository.GetRepository<dmDonVi>().GetAllAsync();
                ViewBag.IdDonVi = new SelectList(listDonVi, "Id", "Name", IdDonVi);
            }
            else { 
                var listDonVi = await _repository.GetRepository<dmDonVi>().GetAllAsync(o=>o.Id == DonVi);
                ViewBag.IdDonVi = new SelectList(listDonVi, "Id", "Name", IdDonVi);
            }
            filterExpression = o => o.IdDonVi == DonVi.Value;
            var temp = await GetRespository().GetAllAsync(filterExpression);
            return View(temp.OrderBy(o => o.Id));
        }

        [Route("nhap" + CRoute, Name = CName + "_Create")]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public async Task<ActionResult> Create(long? IdDonVi)
        {
            ViewBag.Title = "Thêm mới " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            var dsDonVi = await _repository.GetRepository<dmDonVi>().GetAllAsync();
            // lọc tiếp theo người 
            ViewBag.IdCha = new SelectList(dsDonVi, "Id", "Name", IdDonVi);
            return View();
        }
        [Route("nhap" + CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Create, Module = CModule)]
        public async Task<ActionResult> Create(CapQuanLy model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string ma = StringHelper.KillChars(model.Code);
                    //Kiểm tra trùng mã
                    var any = await GetRespository().AnyAsync(o => o.Code == ma);
                    if (any)
                    {
                        ViewBag.Error = "Mã này đã được sử dụng! Vui lòng nhập mã khác!";
                        return View(model);
                    }
                    //Nhập trạng thái bài viết
                    var newItem = NewObject();
                    newItem = model;
                    newItem.Code = StringHelper.KillChars(model.Code);
                    newItem.Name = StringHelper.KillChars(model.Name);
                    newItem.Description = StringHelper.KillChars(model.Description);

                    int result = await GetRespository().CreateAsync(newItem, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Thêm mới thành công " + CText;
                        return RedirectToRoute(CName + "_Index");
                    }
                    else
                    {
                        ViewBag.Error = "Không thêm được " + CText;
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
        [Route("cap-nhat-" + CRoute + "/{ma}", Name = CName + "_Update")]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(long Id, long? idDonVi)
        {
            var editingItem = await GetRespository().ReadAsync(o => o.Id == Id);
            if (editingItem == null)
            {
                TempData["Error"] = "Không tìm thấy " + CText;
                return RedirectToRoute(CName + "_Index");
            }
            var listDv =await _repository.GetRepository<dmDonVi>().GetAllAsync();
            ViewBag.idDonVi = new SelectList(listDv, "Id", "Name", idDonVi);
            BaseView();
            return View(editingItem);
        }
        [Route("cap-nhat-" + CRoute)]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [ValidationPermission(Action = ActionEnum.Update, Module = CModule)]
        public async Task<ActionResult> Update(long Id, CapQuanLy model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Cập nhật trạng thái bài viết
                    var updateItem = await GetRespository().ReadAsync(o => o.Id == Id);
                    if (updateItem == null)
                    {
                        TempData["Error"] = "Không tìm thấy " + CText;
                        return RedirectToRoute(CName + "_Index");
                    }
                    var vld = await GetRespository().AnyAsync(o => o.Code == model.Code);
                    if (vld)
                    {
                        TempData["Error"] = "Đã có chức danh sử dụng mã này : " + CText;
                        return RedirectToRoute(CName + "_Index");
                    }
                    //Không cho sửa mã, nếu cho sửa phải kiểm tra trùng
                    updateItem.Code = StringHelper.KillChars(model.Code);
                    updateItem.Name = StringHelper.KillChars(model.Name);
                    updateItem.Description = StringHelper.KillChars(model.Description);
                    int result = await GetRespository().UpdateAsync(updateItem, AccountId);
                    if (result > 0)
                    {
                        TempData["Success"] = "Ghi nhận thành công!";
                        return RedirectToRoute(CName + "_Index");
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
        [Route("Xoa-" + CRoute + "/{code?}", Name = CName + "_Delete")]
        [HttpPost]
        [ValidationPermission(Action = ActionEnum.Delete, Module = CModule)]
        public async Task<JsonResult> Delete(long code)
        {
            try
            {
                var deleteItem = await GetRespository().ReadAsync(code);
                if (deleteItem != null)
                {
                    if (!CanDelete(deleteItem)) return Json(new { success = false, message = CText + " đang được sử dụng!" });
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

        #region tìm kiếm nâng cao
        [Route("tim-kiem-" + CRoute, Name = CName + "_Search")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> Search(CapQuanLy searchmodel, long? DonVi)
        {
            BaseView();
           
            Expression<Func<CapQuanLy, bool>> filterExpression;
           //if(DonVi != null)
           // {
           //     var listDonVi = await _repository.GetRepository<dmDonVi>().GetAllAsync(o => o.Id == DonVi.Value);
           //     ViewBag.DonVi = new SelectList(listDonVi, "Id", "Name");
           // }
           // else
           // {

           // }
            if (DonVi != null)
            {
                long a = DonVi.Value;
                filterExpression = o => (o.IdDonVi == a && (
                o.Code.Contains(searchmodel.Code)
                 || o.Description.Contains(searchmodel.Description)
                 || o.Name.Contains(searchmodel.Name)
                ));
                var temp = await GetRespository().GetAllAsync(filterExpression);
                return View(temp.OrderBy(o => o.Id));
            }

            filterExpression = o => (
                o.Code.Contains(searchmodel.Code)
                 || o.Description.Contains(searchmodel.Description)
                 || o.Name.Contains(searchmodel.Name)
                );
            var list = await GetRespository().GetAllAsync(filterExpression);
          //  combobox đơn vị;
            var listDonVi = await _repository.GetRepository<dmDonVi>().GetAllAsync(o => o.Id == DonVi.Value);
                ViewBag.IdDonVi = new SelectList(listDonVi, "Id", "Name", DonVi);
            return View(list.OrderBy(o => o.Id));
        }
        #endregion
    }
}