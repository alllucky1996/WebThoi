using Common.CustomAttributes;
using Dung.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Model
{
    [Table("KhachHang")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class KhachHang : Dung
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [Display(Name = "Họ và tên quý khách")]
        [StringLength(60, ErrorMessage = "Họ và tên không được vượt quá 60 ký tự!")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Display(Name = "Số điện thoại quý khách")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [Display(Name = "Địa chỉ E-mail quý khách")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
        [StringLength(60, ErrorMessage = "Địa chỉ E-mail không được vượt quá 60 ký tự!")]
        public string Email { get; set; }
        [Display(Name= "Nhu cầu của quý khách")]
        public string NhuCau { get; set; }

        public DateTime NgayTao { get; set; }
      
        [Display(Name ="Trang thái khách hàng")]
        public TrangThaiKhachHangEnum TrangThaiKhachHang { get; set; }
        public KhachHang()
        {
            NgayTao = DateTime.Now;
            TrangThaiKhachHang = TrangThaiKhachHangEnum.ChuaLienHe;
        }
        public string Describe()
        {
            return "{họ tên: "+Name+",SĐT: "+PhoneNumber+"}";

        }
    }
}
