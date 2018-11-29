using Common.CustomAttributes;
using System.ComponentModel;
namespace Entities.Enums
{
    /// <summary>
    /// Danh sách mã chức năng của hệ thống
    /// </summary>
    public enum ModuleEnum
    {
        //Nhóm chức năng quản trị hệ thống, bắt đầu từ 500
        [ModuleGroupAttribute(ModuleGroupCode = 1, ModuleGroupName = "Quản trị hệ thống")]
        [Description("Quản lý tài khoản")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        Account = 100,
        [ModuleGroupAttribute(ModuleGroupCode = 1, ModuleGroupName = "Quản trị hệ thống")]
        [Description("Quản lý vai trò người dùng (Nhóm người dùng)")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        Role = 101,
        [ModuleGroupAttribute(ModuleGroupCode = 1, ModuleGroupName = "Quản trị hệ thống")]
        [Description("Tham số hệ thống")]
        [ActionAttribute(ActionType.Read, ActionType.Update)]
        SystemInformation = 102,

        [ModuleGroupAttribute(ModuleGroupCode = 1, ModuleGroupName = "Quản trị hệ thống")]
        [Description("Mẫu văn bản")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        MauVanBan = 103,

        //Các chức năng Quản trị danh mục
        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description(" mục tin")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        MucTin = 200,


        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Trạng thái bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        TrangThaiBaiViet = 201,

        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Thao tác chuyển trạng thái ")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        ThaoTacChuyenTrangThai = 202,
        // Văn bản biểu mẫu
        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Tài khoản cán bộ")]
        [Description("Tạo bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        TaoBaiViet = 300,

        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Tài khoản cán bộ")]
        [Description("Duyệt bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        DuyetBaiViet= 301,

        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Tài khoản cán bộ")]
        [Description("Xử lý bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        XuLyBaiViet = 302,

        

        //[ModuleGroupAttribute(ModuleGroupCode = 4, ModuleGroupName = "Tài khoản cán bộ")]
        //[Description("Tạo yêu cầu tài khoản sinh viên")]
        //[ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        //TaoYeuCauSV= 403,

        //[ModuleGroupAttribute(ModuleGroupCode = 4, ModuleGroupName = "Tài khoản cán bộ")]
        //[Description("Duyệt yêu cầu tài khoản sinh viên")]
        //[ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        //DuyetYeuCauSV = 404,

        //[ModuleGroupAttribute(ModuleGroupCode = 4, ModuleGroupName = "Tài khoản cán bộ")]
        //[Description("Xử lý yêu cầu tài khoản sinh viên")]
        //[ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        //XuLyYeuCauSV = 405,

        [ModuleGroupAttribute(ModuleGroupCode = 5, ModuleGroupName = "Thống kê")]
        [Description("Thống kê bài viết")]
        [ActionAttribute(ActionType.Read)]
        ThongKeBaiViet = 501,
        [ModuleGroupAttribute(ModuleGroupCode = 5, ModuleGroupName = "Thống kê")]
        [Description("Tìm kiếm")]
        [ActionAttribute(ActionType.Read)]
        TimKiem = 502,
        [ModuleGroupAttribute(ModuleGroupCode = 5, ModuleGroupName = "Thống kê")]
        [Description("Nhật ký")]
        [ActionAttribute(ActionType.Read)]
        NhatKy = 503,
        // đến đây không phải sửa 
        [ModuleGroupAttribute(ModuleGroupCode = 6, ModuleGroupName = "Khách hàng")]
        [Description("Khách hàng")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        KhachHang = 601,

        // trạng thái theo cách mới
        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Trạng thái bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        TrangThai = 701,

        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Chuyển trạng thái ")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        ChuyenTrangThai = 702,
        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Trạng thái bài viết")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        LoaiTrangThai = 703,
    }
}
