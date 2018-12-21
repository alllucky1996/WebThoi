using Entities.Models;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Entities.Enums;
using Web.Areas.Management.Filters;
using Common.Helpers;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Dung.Model;
using System.Web;
using System.Runtime.InteropServices;
using Entities.Models.SystemManage;

namespace Web.Areas.FrontEnd.Controllers
{
	[RouteArea("FrontEnd", AreaPrefix = "dich-vu")]
	public class HomeController : PublicController
	{
		public const string CName = "HinhAnh";
		public const string CRoute = "hinh-anh";
		public const string CText = " Hình ảnh";
		public static int flag = 0;
		#region index
		[Route(Name = "FrontEnd_Home_Index")]
		public async Task<ActionResult> Index()
		{
			try
			{
				var list = await _repository.GetRepository<CheckImage>().GetAllAsync(o => o.IsDeleted != true);


				ViewBag.Title = "Danh mục" + CText;

				ViewBag.CanCreate = true;
				ViewBag.CName = CName;
				ViewBag.CText = CText;
				if (flag == 0)
				{
					//flag = await run(@"F:\Thay Dung\KPI-Trung P10\DSNS.xls");
					flag++;
				}
				 
				return View(list);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}
		#endregion


		#region Action
		[Route("nhap-" + CRoute, Name = CName + "_MultiCreate")]
		public async Task<ActionResult> MultiCreate()
		{
			ViewBag.Title = "Thêm mới " + CText;
			ViewBag.CName = CName;
			ViewBag.CText = CText;
			return View();
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
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CheckImage model, HttpPostedFileBase file)
		{
			try
			{
				Guid id = Guid.NewGuid();

				if (file == null)
				{
					return Json(new { success = false, message = "Không có file gửi lên" }, JsonRequestBehavior.AllowGet);
				}
				var type = Path.GetExtension(file.FileName);
				var fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Guid.NewGuid().ToString().Replace("-", "_") + type;
				var temp = Server.MapPath("~/Upload/CheckImage/") + fileName;
				file.SaveAs(temp);
				model.Path = "/Upload/CheckImage/" + fileName;
				//Nhập trạng thái bài viết
				var newItem = new CheckImage();
				newItem.Code = StringHelper.KillChars(model.Code);
				newItem.Name = model.Name == null ? Path.GetFileName(fileName) : model.Name;
				newItem.Description = model.Description;
				newItem.Path = model.Path;
				newItem.IsChecked = model.IsChecked;

				int resul = _repository.GetRepository<CheckImage>().Create(newItem, AccountId);
				if (resul > 0)
				{
					TempData["Success"] = "Thêm mới thành công " + CText;
					//return Json(new { success = true, message = TempData["Success"], result = resul }, JsonRequestBehavior.AllowGet);
					return RedirectToRoute(CName + "_Index");
				}
				else
				{
					ViewBag.Error = "Không thêm được " + CText;
					//return Json(new { success = false, message = "Không thêm được " + CText, result = resul }, JsonRequestBehavior.AllowGet);
					return View(model);
				}
			}
			catch (Exception ex)
			{
				ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
				return View(model);
			}
		}
		#endregion
		async Task<int> run(string fname)
		{
			int result = 0;

			//Console.OutputEncoding = Encoding.UTF8;
			Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fname);
			xlWorkbook.WebOptions.Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingUTF8;
			Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;
			//add parent don vi
			//var a = await read_parent_donvi(rowCount, xlRange);
			//add child don vi
			//var child_dv = await read_child_donvi(rowCount, xlRange);

			//Don vi to nhom
			//add child don vi
			//var child1_dv = await read_child1_donvi(rowCount, xlRange);
			//add chuc danh
			var ChucVu = await Read_chucVu(rowCount, xlRange);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			xlApp.Quit();
			Marshal.ReleaseComObject(xlApp);
			return result;
		}

		async Task<int> read_parent_donvi(int rowCount, Microsoft.Office.Interop.Excel.Range xlRange)
		{
			int result = 0;
			List<string> lst = new List<string>();
			List<string> lstDonVi = new List<string>();
			for (int i = 5; i <= rowCount; i++)
			{
				if (xlRange.Cells[i, 8] != null && xlRange.Cells[i, 8].Value2 != null)
				{
					lst.Add(xlRange.Cells[i, 8].Value2.ToString());
				}
			}
			lstDonVi = lst.Distinct().ToList();
			var name_parent = lstDonVi.First();
			foreach (var value in lstDonVi)
			{
				result = await _repository.GetRepository<dmDonVi>().CreateAsync(new dmDonVi() { Name = value, DienThoai = "xxxxxx", Email = "xxxxxx@gmail.com", DiaChi = name_parent }, 0);
				Debug.WriteLine(value);
			}
			return result;
		}


		async Task<int> read_child_donvi(int rowCount, Microsoft.Office.Interop.Excel.Range xlRange)
		{
			List<Handler.obj> lst_child = new List<Handler.obj>();
			List<string> lstDonVi = new List<string>();
			int result = 0;
			Handler.obj obj;
			var list = await _repository.GetRepository<dmDonVi>().GetAllAsync();
			for (int i = 5; i <= rowCount; i++)
			{
				if (xlRange.Cells[i, 7] != null && xlRange.Cells[i, 7].Value2 != null)
				{
					obj = new Handler.obj(xlRange.Cells[i, 7].Value2, xlRange.Cells[i, 8].Value2);
					lst_child.Add(obj);
				}
			}
			List<Handler.obj> lst_child1 = new List<Handler.obj>();
			lst_child1 = (from o in lst_child group o by o.Parent into g select g.First()).ToList();
			foreach (var value in lst_child1)
			{
				var id_par = list.Where(o => o.Name.Equals(value.Child));
				result = await _repository.GetRepository<dmDonVi>().CreateAsync(new dmDonVi() { Name = value.Parent, DienThoai = "xxxxxx", Email = "xxxxxx@gmail.com", DiaChi = value.Child, IdCha = id_par.First().Id }, 0);
				Debug.WriteLine(value.Parent);
			}

			return result;
		}


		async Task<int> read_child1_donvi(int rowCount, Microsoft.Office.Interop.Excel.Range xlRange)
		{
			List<Handler.obj> lst_child = new List<Handler.obj>();
			List<string> lstDonVi = new List<string>();
			int result = 0;
			Handler.obj obj;
			var list = await _repository.GetRepository<dmDonVi>().GetAllAsync();
			for (int i = 5; i <= rowCount; i++)
			{
				if (xlRange.Cells[i, 12] != null && xlRange.Cells[i, 12].Value2 != null)
				{
					obj = new Handler.obj(xlRange.Cells[i, 12].Value2, xlRange.Cells[i, 7].Value2);
					lst_child.Add(obj);
				}
			}
			List<Handler.obj> lst_child1 = new List<Handler.obj>();
			lst_child1 = (from o in lst_child group o by o.Parent into g select g.First()).ToList();
			foreach (var value in lst_child1)
			{
				if (!string.IsNullOrEmpty(value.Parent))
				{
					var id_par = list.Where(o => o.Name.Equals(value.Child));
					result = await _repository.GetRepository<dmDonVi>().CreateAsync(new dmDonVi() { Name = value.Parent, DienThoai = "xxxxxx", Email = "xxxxxx@gmail.com", DiaChi = value.Child, IdCha = id_par.First().Id }, 0);
					Debug.WriteLine(value.Parent);
				}
			}

			return result;
		}

		async Task<int> Read_chucVu(int rowCount, Microsoft.Office.Interop.Excel.Range xlRange)
		{
			int result = 0;
			List<string> lst = new List<string>();
			List<string> lstdonvi = new List<string>();
			for (int i = 5; i <= rowCount; i++)
			{
				if (xlRange.Cells[i, 6] != null && xlRange.Cells[i, 6].value2 != null)
				{
					lst.Add(xlRange.Cells[i, 6].value2.ToString());
				}
			}
			lstdonvi = lst.Distinct().ToList();
			int index = 0;
			foreach (var value in lstdonvi)
			{
				if (!string.IsNullOrEmpty(value))
				{
					result = await _repository.GetRepository<CapQuanLy>().CreateAsync(new CapQuanLy() { Code = "xxxxx", Name = value }, 0);
					Debug.WriteLine("\n" + value + "=======" + index);
					index++;
				}
			}
			return result;
		}
	}
}