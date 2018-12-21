using Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Tài khoản người dùng
    /// </summary>
    [Table("Account")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class Account : Entity
    {
        [Key]
        //[Column(Order = 0), Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên!")]
        [Display(Name = "Họ và tên")]
        [StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự!")]
        public string FullName { get; set; }

      
        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        [Display(Name = "Địa chỉ E-mail!")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Địa chỉ E-mail không được vượt quá 50 ký tự!")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
       // [StringLength(32, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên và ít hơn 32 ký tự!")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        [Column(TypeName = "nvarchar")]
        public string Password { get; set; }

        //Là tài khoản quản trị nội dung
        public bool IsManageAccount { get; set; }
        //Là tài khoản trang ngoài (quản trị danh mục)
        public bool IsNormalAccount { get; set; }
        //Là tài khoản chuyên gia
        [Display(Name = "Là chuyên gia")]
        public bool IsExpertsAccount { get; set; }
        
        //Ngày tạo
        public DateTime CreateDate { get; set; }

        [StringLength(200, ErrorMessage = "Đường dẫn ảnh đại diện không được vượt quá 200 ký tự!")]
        [Display(Name = "Ảnh đại diện")]
        public string ProfilePicture { get; set; }

        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        [Column(TypeName = "nvarchar")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        //Important - always use ICollection, not IEnumerable for Navigation properties and make them virtual - thanks to this ef will be able to track changes

        //Đoạn thêm mới các thông tin
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public bool? Sex { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự!")]
        public string Address { get; set; }

        [Display(Name = "Đơn vị")]
        public long? IdDonVi { get; set; }
        [ForeignKey("IdDonVi")]
        public virtual dmDonVi DonVi { get; set; }
        /// <summary>
        /// Cấp quản lý
        /// </summary>
        /// 
        [Display(Name = "Cấp quản lý")]
        public string CapQuanLy { get; set; }

        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + Id + "\", Name : \"" + FullName + "\", { Email : \"" + Email + "\" }";
        }
    }
}
