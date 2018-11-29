using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Quên mật khẩu
    /// Mã kích hoạt có giá trị trong 24 giờ
    /// </summary>
    [Table("ForgetPassword")]
    public class ForgetPassword : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long AccountId { get; set; }
        //Thời gian yêu cầu khôi phục mật khẩu
        public DateTime RequestTime { get; set; }
        //Mã xác nhận
        [StringLength(32)]
        [Column(TypeName = "nvarchar")]
        public string ActiveCode { get; set; }
        //Mật khẩu tạm (đã mã hóa)
        [StringLength(32)]
        [Column(TypeName = "nvarchar")]
        public string TemporaryPassword { get; set; }
        /// <summary>
        /// Trạng thái
        /// 0: Vừa gửi yêu cầu khôi phục mật khẩu
        /// 1: Đã khôi phục mật khẩu thành công
        /// 2: Khôi phục mật khẩu không thành công
        /// 3: Đã hết hạn
        /// 4: Gửi email xác nhận yêu cầu khôi phục mật khẩu không thành công
        /// </summary>
        [Column(TypeName = "tinyint")]
        public byte Status { get; set; }
        //Thời gian xác nhận mật khẩu mới thành công
        public DateTime? ActiveTime { get; set; }
        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        public string RequestIp { get; set; }

        public string Describe()
        {
            return "{ AccountId : \"" + AccountId + "\", RequestTime : \"" + RequestTime + "\" }";
        }
    }
}
