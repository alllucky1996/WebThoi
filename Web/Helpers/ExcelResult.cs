using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    //NamMV
    //Cách dùng
    // public ActionResult ExcelExport()
        //{
        //    var viewModel = (ListAssignByGradeViewModel)Session["ListAssignByGradeViewModel"];
        //    return new ExcelResult
        //        (
        //            ControllerContext,
        //           "ListAssignByGradeDetail",
        //            "danh-sach-giao-vien-phan-theo-xe.xls",
        //           viewModel
        //        );
        //}
    //cách khác ko dùng hàm này:
    //public void ExportToExcel()
    //    {
    //        //var teaches = _repository.GetRepository<Teacher>().GetAll(o => (!o.Deleted.HasValue || o.Deleted.Value == false)).ToList();
    //        var assignDetails = Session["assignDetail"] as ICollection<AssignDetail>;
    //        var grid = new System.Web.UI.WebControls.GridView();
    //        int i = 1;
    //        grid.DataSource = from d in assignDetails
    //                          select new
    //                          {
    //                              STT = i++,
    //                              SoDangKyXe = d.Car.RegistrationNumber,
    //                              GiaoVien = d.Teacher.Name,
    //                              SoHocVien = d.TraineeNumber,
    //                              GhiChu = d.Note
    //                          };
    //        grid.DataBind();
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", "attachment; filename=bang-phan-cong-xe-giao-vien.xls");
    //        Response.ContentType = "application/excel";
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        grid.RenderControl(htw);
    //        Response.Write(sw.ToString());
    //        Response.End();
    //    }
    public class ExcelResult : ActionResult
    {
        string _fileName;
        string _viewName;
        object _model;
        ControllerContext _context;
        public ExcelResult(ControllerContext context, string viewName, string fileName, object model)
        {
            this._context = context;
            this._fileName = fileName;
            this._viewName = viewName;
            this._model = model;
        }
        public string RenderRazorViewToString()
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(_context, _viewName);
                var vdd = new ViewDataDictionary<object>(_model);
                var viewContext = new ViewContext(_context, viewResult.View, vdd, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(_context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        void WriteFile(string content)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=\"" + _fileName + "\"");
            context.Response.Charset = "";
            context.Response.ContentType = "application/ms-excel";
            context.Response.Write(content);
            context.Response.End();
        }
        public override void ExecuteResult(ControllerContext context)
        {
            string content = this.RenderRazorViewToString();
            this.WriteFile(content);
        }
    }
}