using System.ComponentModel;
namespace Entities.Enums
{
    /// <summary>
    /// Danh sách các nhóm chức năng
    /// </summary>
    public enum ModuleGroupEnum
    {
        [Description("Quản trị hệ thống")]
        QuanTriHeThong = 1,
        [Description("Danh mục")]
        QuanTriDanhMuc = 2,
        [Description("Tài khoản cán bộ")]
        YeuCau = 3,
        [Description("Tài khoản sinh viên")]
        YeuCauSV = 4,
        [Description("Thống kê")]
        ThongKe = 5
    }
}
