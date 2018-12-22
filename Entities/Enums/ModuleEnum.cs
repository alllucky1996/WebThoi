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
        [Description("Đơn vị")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        DonVi = 103,

        //Các chức năng Quản trị danh mục
        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("Mẫu văn bản")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        MauVanBan = 200,

        // quản trị KPI
        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("KPI cá nhân")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        KPI = 300,

        [ModuleGroupAttribute(ModuleGroupCode = 2, ModuleGroupName = "Quản trị danh mục")]
        [Description("KPI DonVi")]
        [ActionAttribute(ActionType.Read, ActionType.Create, ActionType.Update, ActionType.Delete)]
        KPIDonVi = 300,
        
        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Thống kê")]
        [Description("Thống kê")]
        [ActionAttribute(ActionType.Read)]
        ThongKe = 501,

        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Thống kê")]
        [Description("Tìm kiếm")]
        [ActionAttribute(ActionType.Read)]
        TimKiem = 502,
        [ModuleGroupAttribute(ModuleGroupCode = 3, ModuleGroupName = "Thống kê")]
        [Description("Nhật ký")]
        [ActionAttribute(ActionType.Read)]
        NhatKy = 503
    }
}
