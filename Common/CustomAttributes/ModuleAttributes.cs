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
    /// Enum để đánh dấu chức năng thuộc nhóm chức năng nào
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ModuleGroupAttribute : System.Attribute
    {
        public string ModuleGroupName { get; set; }
        public int ModuleGroupCode { get; set; }
        public ModuleGroupAttribute()
        {
        }
    }
}
