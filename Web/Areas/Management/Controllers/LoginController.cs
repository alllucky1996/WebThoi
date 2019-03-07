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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
                   // sửa truy vấn này vs fornt end 
                    var account = await _repository.GetRepository<Account>().ReadAsync(o => (o.Email == email && o.Password == password && o.IsManageAccount == true));
                  
                    Session.Clear();

                    if (account == null)
                    {
                        ModelState.AddModelError(string.Empty, "Sai địa chỉ e-mail hoặc mật khẩu!");
                        ViewBag.Error = "Sai địa chỉ e-mail hoặc mật khẩu!";
                        return View(model);
                    }
                    else
                    {
                        
                        Session[SessionEnum.Email] = account.Email;
                        Session[SessionEnum.AccountId] = account.Id;
                        Session[SessionEnum.AccountName] = account.FullName;
                        Session[SessionEnum.ProfilePicture] = account.ProfilePicture;
                        Session[SessionEnum.IsExpertsAccount] = account.IsExpertsAccount;
                        Session[SessionEnum.IsManageAccount] = account.IsManageAccount;
                        Session[SessionEnum.AccountType] = "Admin";
                        Session[SessionEnum.AccountDv] = account.IdDonVi;
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
            if (!key.Equals("dung"))
            {
                return RedirectToAction("Index", "Login");
            }
            // account
            if (_repository.GetRepository<Account>().Any(o => o.Id == 1))
            {
                return RedirectToAction("Index", "Login");
            }
            var account = new Account()
            {
                FullName = "Nguyễn Anh Dũng",
                Password = StringHelper.stringToSHA512("123456").ToLower(),
                Email = "itfa.ahihi@gmail.com",
                CreateDate = DateTime.Now,
                IsManageAccount = true,
                IsNormalAccount = false,
                PhoneNumber = "0978132474"
            };
            await _repository.GetRepository<Account>().CreateAsync(account, 0);
            // role
            var role = new Role()
            {
                Name = "Quản trị hệ thống"
            };
            await _repository.GetRepository<Role>().CreateAsync(role, 1);
            //gán quyền
            var accountRole = new AccountRole()
            {
                AccountId = 1,
                RoleId = 1
            };
            await _repository.GetRepository<AccountRole>().CreateAsync(accountRole, 1);
            // app module quyền
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
            var moduleRole3 = new ModuleRole()
            {
                RoleId = 1,
                ModuleCode = "DonVi",
                Create = 1,
                Read = 1,
                Update = 1,
                Delete = 1
            };
            await _repository.GetRepository<ModuleRole>().CreateAsync(moduleRole3, 1);
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

            var anyDonvi = await _repository.GetRepository<dmDonVi>().CountAsync();
            if(anyDonvi<1)
                await run(Server.MapPath("~/Upload/ExcelFiles/DSNS.xls"));

            // quay về index để đăng nhập
            return RedirectToAction("Index", "Login");
        }

        #region init from excel
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
            var a = await read_parent_donvi(rowCount, xlRange);

            //add child don vi
            var child_dv = await read_child_donvi(rowCount, xlRange);

            //Don vi to nhom
            var child1_dv = await read_child1_donvi(rowCount, xlRange);

            //add chuc danh
            var ChucVu = await Read_chucVu(rowCount, xlRange);

            // add national
            //var national = await Read_national(rowCount, xlRange);
            DateTime d = new DateTime(1977, 5, 9);
            try
            {
                result = await _repository.GetRepository<Account>().CreateAsync(new Account()
                {
                    Code = 270800,
                    FullName = "Ma Thị Hồng Vân",
                    Sex = false,
                    Email = "XXX@gmail.com",
                    IsExpertsAccount = true,
                    IsManageAccount = true,
                    IsNormalAccount = true,
                    PhoneNumber = "0000000000",
                    Password = "123123",
                    Address = "XXX",
                    CreateDate = DateTime.Now,
                    ProfilePicture = "XXX",
                    DateOfBirth = d,
                    //IDCapQuanLy = 30,
                    //IdDonVi = 7,
                }, 0);
            }
            catch (Exception ex)
            {

                throw;
            }

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
            List<int> r = new List<int>();
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
               // result = await _repository.GetRepository<dmDonVi>().CreateAsync(new dmDonVi() { Name = value, DienThoai = "xxxxxx", Email = "xxxxxx@gmail.com", DiaChi = name_parent }, 0);
               r.Add( await _repository.GetRepository<dmDonVi>().CreateAsync(new dmDonVi() { Name = value, DienThoai = "xxxxxx", Email = "xxxxxx@gmail.com", DiaChi = name_parent }, 0));
                Debug.WriteLine("add thành công: "+value);
            }
            return r.Count();
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
                    result = await _repository.GetRepository<CapQuanLy>().CreateAsync(new CapQuanLy() { Name = value }, 0);
                    Debug.WriteLine(value + "=======" + index);
                    index++;
                }
            }
            return result;
        }

        async Task<int> Read_national(int rowCount, Microsoft.Office.Interop.Excel.Range xlRange)
        {
            int result = 0;
            List<string> lst = new List<string>();
            List<string> lstdonvi = new List<string>();
            for (int i = 5; i <= rowCount; i++)
            {
                if (xlRange.Cells[i, 10] != null && xlRange.Cells[i, 10].value2 != null)
                {
                    lst.Add(xlRange.Cells[i, 10].value2.ToString());
                }
            }
            lstdonvi = lst.Distinct().ToList();
            int index = 0;
            foreach (var value in lstdonvi)
            {
                result = await _repository.GetRepository<national>().CreateAsync(new national() { Name = value }, 0);
                Debug.WriteLine(value + "=======" + index);
                index++;
            }
            return result;
        }
        #endregion
    }
}