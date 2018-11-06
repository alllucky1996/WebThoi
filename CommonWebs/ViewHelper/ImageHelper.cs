using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWebs.ViewHelper
{
    public class ImageHelper
    {
        public static string ViewImage(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            int index = str.IndexOf("/Upload/");
            string temp =  str.Substring(index - 8);
            return temp;
        }
    }
}
