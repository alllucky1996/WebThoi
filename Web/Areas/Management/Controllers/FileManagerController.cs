using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Common.Helpers;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    [RoutePrefix("quan-ly-tep-tin")]
    public class FileManagerController : BaseController
    {
        [Route("danh-sach-tep-tin", Name = "FileManagerIndex")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateFolder(string folderName)
        {
            var xxx = StringHelper.StringFilter2(folderName);
            xxx = xxx.TrimStart('-');
            string message = "";
            try
            {
                string filePath = Server.MapPath("~/Uploads/images/" + xxx + "/");
                if (!Directory.Exists(filePath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(filePath);
                }
                message = "Tạo thư mục thành công";
            }
            catch (Exception ex)
            {
                message = "Tạo thư mục không thành công. Lỗi: " + ex.Message;
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult UploadFile(string folderId)
        {
            var file = Request.Files["file"];
            if (file != null && file.ContentLength > 0)
            {
                //string filePath = Server.MapPath("~/Uploads/images/");
                string filePath = Server.MapPath("~/Uploads/images/" + folderId + "/");
                string fileName = CommonHelper.ToURL(Path.GetFileNameWithoutExtension(file.FileName), 0) + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(filePath, Path.GetFileName(fileName));
                if (System.IO.File.Exists(fullPath))//Kiểm tra file đã tồn tại
                {
                    fileName = CommonHelper.ToURL(Path.GetFileNameWithoutExtension(file.FileName), 0) + CommonHelper.RandomString(10) + Path.GetExtension(file.FileName);
                    fullPath = Path.Combine(filePath, fileName);
                }                
                file.SaveAs(fullPath);

                string tempmessage = "OK";
                return Json(tempmessage);
            }
            return Json("Tệp tin không hợp lệ.");
        }

        [HttpGet]
        public ActionResult GetFolders()
        {
            var folders = new List<FOLDER>();
            string filePath = Server.MapPath("~/Uploads/images/");
            DirectoryInfo dInfo = new DirectoryInfo(filePath);
            DirectoryInfo[] subdirs = dInfo.GetDirectories();
            folders = GetListFolder(subdirs, "0");
            var list = new List<TreeNode>();
            var cRoot = new TreeNode { Text = "Root", Id = "0", Parent = null };
            list.Add(cRoot);
            list.AddRange(folders.Select(folder => new TreeNode { Id = folder.ID, Text = folder.NAME, Parent = folder.PARENTID }));
            //build the tree
            var tree = list.ToTree();
            ViewBag.TreeView = tree;
            return View("TreeView");
        }
        private List<FOLDER> GetListFolder(DirectoryInfo[] subdirs, string parentId)
        {
            var folders = new List<FOLDER>();
            if (subdirs != null && subdirs.Any())
            {
                foreach (var item in subdirs)
                {
                    FOLDER a = new FOLDER();
                    a.NAME = item.Name;
                    a.ID = item.Name;
                    a.PARENTID = parentId;
                    folders.Add(a);
                    if (item.GetDirectories() != null && item.GetDirectories().Any())
                        folders.AddRange(GetListFolder(item.GetDirectories(), item.Name));
                }
            }
            return folders;
        }

        [HttpGet]
        public ActionResult ListFile(string folderId)
        {
            var imgPath = "";
            string filePath = "";
            if (!string.Equals(folderId, "/", StringComparison.CurrentCultureIgnoreCase))
            {
                var folder = new FOLDER()
                {
                    ID = folderId,
                    NAME = "Test folder"
                };
                ViewBag.FolderName = folder.NAME;
                ViewBag.FolderId = folderId;
                var tmp = folderId.TrimStart('/');
                imgPath = "/Uploads/images/" + tmp + "/";
                filePath = Server.MapPath("~/Uploads/images/" + tmp + "/");               
            }
            else
            {
                ViewBag.SubUrl = "null";
                ViewBag.FolderName = "Root";
                imgPath = "/Uploads/images/";
                filePath = Server.MapPath("~/Uploads/images/");
            }
            var files = new List<FILE>(); //GetFileByFolderId(folderId);
            //var imgPath = "/Uploads/images/" + folderId + "/";
            //string filePath = Server.MapPath("~/Uploads/images/" + folderId + "/");
            try
            {
                DirectoryInfo d = new DirectoryInfo(filePath);
                foreach (var f in d.GetFiles())
                {
                    FILE file = new FILE();
                    file.NAME = f.Name;
                    file.ID = f.Name;
                    file.FOLDERID = folderId;
                    file.FULLPATH = folderId + "/" + f.Name;
                    //file.FULLPATH = folderId + f.Name;
                    file.URL = imgPath + f.Name;
                    files.Add(file);
                }
            }
            catch
            {

            }
            ViewBag.Files = files;
            return View("ListFile");
        }

        [HttpPost]
        public JsonResult DeleteFile(string fileId)
        {
            string message = "";
            try
            {
                string filePath = Server.MapPath("~/Uploads/images/" + fileId);

                // Get the attributes of the file
                var attr = System.IO.File.GetAttributes(filePath);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    System.IO.File.SetAttributes(filePath, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                System.IO.File.Delete(filePath);
                message = "Xóa ảnh thành công!";
            }
            catch (Exception ex)
            {
                message = "Xóa ảnh không thành công! Lỗi: " + ex.Message;
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult DeleteFolder(string folderId)
        {
            string message = "";
            try
            {
                string filePath = Server.MapPath("~/Uploads/images/" + folderId + "/");
                DeleteDirectory(filePath, true);
                message = "Xóa thư mục thành công!";
            }
            catch (Exception ex)
            {
                message = "Xóa thư mục không thành công! Lỗi: " + ex.Message;
            }
            return Json(message);
        }
        /*
         * 
         // Delete all files from the folder 'c:\Games', but
        // keep all sub-folders and its files
        DeleteDirectory(@"c:\Games");
 
        // Delete the folder 'c:\Projects' and all of its content
        DeleteDirectory(@"c:\Projects", true);
 
        // Delete all files from the folder 'c:\Software', but
        // keep all sub-folders and its files
        DeleteDirectory(@"c:\Software", false);
         * /*/
        private static void DeleteDirectory(string path)
        {
            DeleteDirectory(path, false);
        }

        private static void DeleteDirectory(string path, bool recursive)
        {
            // Delete all files and sub-folders?
            if (recursive)
            {
                // Yep... Let's do this
                var subfolders = Directory.GetDirectories(path);
                foreach (var s in subfolders)
                {
                    DeleteDirectory(s, recursive);
                }
            }

            // Get all files of the folder
            var files = Directory.GetFiles(path);
            foreach (var f in files)
            {
                // Get the attributes of the file
                var attr = System.IO.File.GetAttributes(f);

                // Is this file marked as 'read-only'?
                if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Yes... Remove the 'read-only' attribute, then
                    System.IO.File.SetAttributes(f, attr ^ FileAttributes.ReadOnly);
                }

                // Delete the file
                System.IO.File.Delete(f);
            }

            // When we get here, all the files of the folder were
            // already deleted, so we just delete the empty folder
            Directory.Delete(path);
        }

        public ActionResult TestPopup()
        {
            return View();
        }

    }
}