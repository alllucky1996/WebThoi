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
    //[Table("DonVi")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class dmDonVi : Dung
    {
        [Key]
        public long Id { get; set; }
        [Display(Name ="Mã")]
        public string Code { get; set; }
        #region const
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name="Tên")]
        public string Name { get; set; }
        #endregion
        [StringLength(250)]
        public string DiaChi { get; set; }

        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        public string DienThoai { get; set; }

        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        public string Email { get; set; }
        

        public long? IdCha { get; set; }
        [ForeignKey("IdCha")]
        public virtual dmDonVi DonVi { get; set; }
        public dmDonVi()
        {
            CreateDate = DateTime.Now;
        }
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
}
