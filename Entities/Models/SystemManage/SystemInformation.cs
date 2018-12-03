using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Thông tin cấu hình hệ thống
    /// </summary>
    [Table("SystemInformation")]
    public class SystemInformation : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên trang web")]
        [Display(Name = "Tên trang web")]
        [StringLength(200, ErrorMessage = "Tên trang web không được vượt quá 200 ký tự!")]
        public string SiteName { get; set; }

        [Display(Name = "Khẩu hiệu")]
        [StringLength(250, ErrorMessage = "Khẩu hiệu không được vượt quá 250 ký tự!")]
        public string Slogan { get; set; }

        [Display(Name = "Ảnh logo")]
        [StringLength(200, ErrorMessage = "Đường dẫn ảnh logo không được vượt quá 200 ký tự!")]
        public string Logo { get; set; }

        [Display(Name = "Thông tin giới thiệu (cuối trang)")]
        [StringLength(500, ErrorMessage = "Thông tin giới thiệu không được vượt quá 500 ký tự!")]
        public string Copyright { get; set; }

        [Display(Name = "Địa chỉ Email")]
        [StringLength(50, ErrorMessage = "Địa chỉ email không được vượt quá 50 ký tự!")]
        [Column(TypeName = "nvarchar")]
        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        public string Email { get; set; }

        [Display(Name = "Tên công ty")]
        [StringLength(255, ErrorMessage = "Tên công ty không được vượt quá 255 ký tự!")]
        public string CompanyName { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự!")]
        public string Address { get; set; }

        [Display(Name = "Điện thoại")]
        [StringLength(40, ErrorMessage = "Điện thoại không được vượt quá 40 ký tự!")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Đường dây nóng")]
        [StringLength(40, ErrorMessage = "Điện thoại Đường dây nóng không được vượt quá 40 ký tự!")]
        public string HotLine { get; set; }

        [Display(Name = "Địa chỉ trang web")]
        [StringLength(255, ErrorMessage = "Địa chỉ trang web không được vượt quá 255 ký tự!")]
        public string WebsiteAddress { get; set; }

        [Display(Name = "Địa chỉ Facebook")]
        [StringLength(255, ErrorMessage = "Địa chỉ Facebook không được vượt quá 255 ký tự!")]
        public string FacebookPage { get; set; }

        [Display(Name = "Facebook AppId")]
        [StringLength(20, ErrorMessage = "Facebook AppId không được vượt quá 20 ký tự!")]
        public string FacebookAppId { get; set; }

        //SMTP
        [Display(Name = "Địa chỉ email SMTP")]
        [Column(TypeName = "nvarchar")]
        [StringLength(50, ErrorMessage = "Địa chỉ email SMTP không được vượt quá 50 ký tự!")]
        [RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
        public string SMTPEmail { get; set; }

        [Display(Name = "Mật khẩu email SMTP")]
        [StringLength(32, ErrorMessage = "Mật khẩu email SMTP không được vượt quá 32 ký tự!")]
        [Column(TypeName = "nvarchar")]
        public string SMTPPassword { get; set; }

        [Display(Name = "Tên email SMTP")]
        [StringLength(100, ErrorMessage = "Tên email SMTP không được vượt quá 100 ký tự!")]
        public string SMTPName { get; set; }

        //Bảng giá cước
        [Display(Name = "Bảng giá cước")]
        public long? TaxiFare { get; set; }

        //=================Site map

        //Menu chính
        public long? MainMenu { get; set; }

        //Menu con bên phải (bên cạnh menu chính)
        public long? RightMenu { get; set; }

        //Album để hiển thị slide show ở trang chủ
        public long? AlbumSlide { get; set; }

        //Album ảnh các đối tác
        public long? BottomAlbum { get; set; }

        //Danh sách liên kết cuối trang
        public long? BottomMenu { get; set; }

        public string Describe()
        {
            return "SystemInformation";
        }
    }
}
