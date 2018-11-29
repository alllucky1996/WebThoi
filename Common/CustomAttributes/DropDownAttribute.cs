using System;

namespace Common.CustomAttributes
{
    //Lớp này của Lê Việt Nam
    [AttributeUsage(AttributeTargets.All)]
    public class DropDownAttribute : System.Attribute
    {
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public DropDownAttribute()
        {
        }
    }
}
