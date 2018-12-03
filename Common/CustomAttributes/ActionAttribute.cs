using System;

namespace Common.CustomAttributes
{
    //[ModuleAttribute(ModuleGroupName = "Quản trị hệ thống", ModuleName = "Quản lý người dùng", OrdinalNumber = 1, Actions = new byte[] { 1, 2, 3, 4, 7 })]
    //[AttributeUsage(AttributeTargets.All)]
    //public class ModuleAttribute : System.Attribute
    //{
    //    public string ModuleGroupName { get; set; }
    //    public string ModuleName { get; set; }
    //    public int OrdinalNumber { get; set; }
    //    //public List<string> Action { get; set; }
    //    public byte[] Actions { get; set; }
    //    public ModuleAttribute(byte[] actions)
    //    {
    //        this.Actions = actions;
    //    }
    //    public ModuleAttribute()
    //    {
    //    }
    //}
    /// <summary>
    /// Enum để đánh dấu 1 chức năng có những hành động gì: Thêm, sửa, xóa, duyệt....
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ActionAttribute : Attribute
    {
        public int[] Actions { get; set; }
        public ActionAttribute(params int[] actions)
        {
            this.Actions = actions;
        }
        //public ActionAttribute()
        //{
        //}
    }
}
