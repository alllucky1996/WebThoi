using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWebs
{
    public class fileHelper
    {
        public static string FileName(string type= null)
        {
            string temp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff_") + Guid.NewGuid().ToString().Replace("-", "_");
            if ( type!= null)
            {
                return temp + type;
            }
            return temp; 
        }

    }
}
