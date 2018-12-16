using Dung.Model;
using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class HomeController : BaseController
    {
        [Route(Name = "ManagementHome")]
        public async Task<ActionResult> Index(String Code)
        {
            if (Code == "dung")
            {
                InitDB();
                return Json(new { success = true, message = "Khỏi tạo 'Images' thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        void InitDB()
        {
            for (int i = 1; i < 100; i++)
            {
                var ni = new CheckImage() {Code= i.ToString(),Name="Hình 001",Path="#",Description= "Mô tả hình Hình 001" ,IsChecked=(i%2==0?true:false) };
                _repository.GetRepository<CheckImage>().Create(ni, 0);
            }
        }

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