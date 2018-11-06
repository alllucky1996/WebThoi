using Common.Helpers;
using Dung.Model;
using Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Management.Helpers;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class ImageController : BaseController
    {
        public const string CName = "Image";
        public const string CRoute = "hinh-anh";
        public const string CText = " Hình ảnh";
        public IGenericRepository<CheckImage> GetRespository()
        {
            return _repository.GetRepository<CheckImage>();
        }
        public static CheckImage NewObject()
        {
            return new CheckImage();
        }

        public bool CanDelete(CheckImage deleteItem)
        {
            //if (deleteItem.YeuCaus != null && deleteItem.YeuCaus.Any())
            //     return false;
            return true;
        }


        [Route("danh-muc-" + CRoute, Name = CName + "_Index")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var list = await GetRespository().GetAllAsync(o => o.IsDeleted != true);
                ViewBag.Title = "Danh mục" + CText;
                ViewBag.CanDelete = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Delete);
                ViewBag.CanCreate = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Create);
                ViewBag.CanUpdate = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Update);
                ViewBag.CName = CName;
                ViewBag.CText = CText;
                return View(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        [Route("danh-sach-" + CRoute, Name = CName + "_Table")]
        public async Task<ActionResult> Table()
        {
            try
            {
                var list = await GetRespository().GetAllAsync(o => o.IsDeleted != true);
                ViewBag.Title = "Danh mục" + CText;
                ViewBag.CanDelete = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Delete);
                ViewBag.CanCreate = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Create);
                ViewBag.CanUpdate = true;// RoleHelper.CheckPermission(CModule, ActionEnum.Update);
                ViewBag.CName = CName;
                ViewBag.CText = CText;
                return View(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        [Route("nhap" + CRoute, Name = CName + "_Create")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Title = "Thêm mới " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View();
        }
        [Route("nhap" + CRoute)]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(CheckImage model, HttpPostedFileBase file)
        {

            try
            {
                string id;
                try
                {
                    id = (GetRespository().GetAll().OrderByDescending(o => o.Id).FirstOrDefault().Id+1).ToString();
                }
                catch 
                {
                    id = "BEGIN";
                }
                if (file == null)
                {
                    return Json(new { success = false, message = "Không có file gửi lên" }, JsonRequestBehavior.AllowGet);
                }
                var type = Path.GetExtension(file.FileName);
                string temp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") /*+ (DateTime.Now.Ticks).ToString()*/ + type;
                var fullName = Server.MapPath("~/Upload/CheckImage/") + temp;
                file.SaveAs(fullName);
                // FileName = Path.GetFileName(fullName);
                model.Path = "/Upload/CheckImage/" + temp;
                //Nhập trạng thái bài viết
                var newItem = NewObject();
                newItem.Code = StringHelper.KillChars(model.Code);
                newItem.Name = model.Name == null ? id: model.Name;
                newItem.Description = model.Description;
                newItem.Path = model.Path;
                newItem.IsChecked = model.IsChecked;

                int resul = GetRespository().Create(newItem, AccountId);
                if (resul > 0)
                {
                    TempData["Success"] = "Thêm mới thành công " + CText;
                    return Json(new { success = true, message = TempData["Success"], result = resul }, JsonRequestBehavior.AllowGet);
                    // return RedirectToRoute(CName + "_Index");
                }
                else
                {
                    ViewBag.Error = "Không thêm được " + CText;
                    return Json(new { success = false, message = "Không thêm được " + CText, result = resul }, JsonRequestBehavior.AllowGet);
                    //return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                return View(model);
            }

        }
        // dưới này chưa làm đến ms chỉ gen ra thôi 
        [Route("cap-nhat-" + CRoute + "/{ma}", Name = CName + "_Update")]
        public async Task<ActionResult> Update(long ma)
        {
            // truyền lên id
            var editingItem = await GetRespository().ReadByKeyAsync(ma);
            if (editingItem == null)
            {
                TempData["Error"] = "Không tìm thấy " + CText;
                return RedirectToRoute(CName + "_Index");
            }
            ViewBag.Title = "Sửa " + CText;
            ViewBag.CName = CName;
            ViewBag.CText = CText;
            return View(editingItem);
        }
        [Route("cap-nhat-" + CRoute)]
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Update(long ma, CheckImage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Cập nhật trạng thái bài viết
                    var updateItem = await GetRespository().ReadByKeyAsync(ma);
                    if (updateItem == null)
                    {
                        TempData["Error"] = "Không tìm thấy " + CText;
                        return RedirectToRoute(CName + "_Index");
                    }
                    //Không cho sửa mã, nếu cho sửa phải kiểm tra trùng
                    //deleteItem.Ma = StringHelper.KillChars(model.Ma);
                    //  var newItem = NewObject();
                    updateItem.Code = StringHelper.KillChars(model.Code);
                    updateItem.Name = StringHelper.KillChars(model.Name);
                    updateItem.Description = model.Description;
                    updateItem.Path = model.Path;

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
        // delete đã test ok
        [Route("Xoa-" + CRoute + "/{code?}", Name = CName + "_Delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(string code)
        {
            try
            {
                var deleteItem = await GetRespository().ReadByKeyAsync(long.Parse(code));
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


    }
}