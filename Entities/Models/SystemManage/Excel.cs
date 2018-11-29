using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public class Excel
    {
        public HttpPostedFileBase ExcelFile { get; set; }

        public string ExcelTable { get; set; }
        public DataTable Data { get; set; }

    }
}
