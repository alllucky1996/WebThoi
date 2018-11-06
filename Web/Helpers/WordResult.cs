using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Helpers
{
    public class WordResult : ActionResult
    {
         string _fileName;
        string _viewName;
        object _model;
        ControllerContext _context;
        dynamic _viewData;
        public WordResult(ControllerContext context, string viewName, string fileName, object model, ViewDataDictionary viewData)
        {
            this._context = context;
            this._fileName = fileName;
            this._viewName = viewName;
            this._model = model;
            this._context.Controller.ViewData = viewData;
        }
        public WordResult(ControllerContext context, string viewName, string fileName, object model)
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
            //context.Response.ContentType = "application/msword";
            context.Response.ContentType = "application/vnd.ms-word";
            context.Response.Write(content);
            context.Response.End();
            context.Response.Flush();
        }
        public override void ExecuteResult(ControllerContext context)
        {
            string content = this.RenderRazorViewToString();
            this.WriteFile(content);
        }
    }
}