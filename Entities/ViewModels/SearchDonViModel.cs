using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class SearchDonViModel
    {
        [Display(Name = "Mã")]
        public string Code { get; set; }
      
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
       
        public string DiaChi { get; set; }
        
        public string DienThoai { get; set; }

        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        public string Email { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Cấp đơn vị")]
        public int CapDV { get; set; }
    }
}
