using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
		 public class Account_User : Entity
		 {

					[Key]
					public long ID { set; get; }

					[RegularExpression(@"^([\w\!\#$\%\&\'*\+\-\/\=\?\^`{\|\}\~]+\.)*[\w\!\#$\%\&\'‌​*\+\-\/\=\?\^`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[‌​a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$", ErrorMessage = "Địa chỉ E-mail không hợp lệ!")]
					[Display(Name = "Địa chỉ E-mail!")]
					[Required(ErrorMessage = "Vui lòng nhập địa chỉ email!")]
					[Column(TypeName = "nvarchar")]
					[StringLength(50, ErrorMessage = "Địa chỉ E-mail không được vượt quá 50 ký tự!")]
					public string Email { get; set; }

					[Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
					[Display(Name = "Password")]
					//[StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự!")]
					public string Password { set; get; }

					public DateTime Created_at { set; get; }

					public DateTime Updated_at { set; get; }

					[Required]
					public Boolean Is_deleted { set; get; }

					public int ID_User { set; get; }
					[ForeignKey("ID_User")]
					public virtual Account Account { set; get; }

					public Account_User()
					{
							 Created_at = DateTime.Now;
							 Is_deleted = false;
					}
					public string Describe()
					{
							 throw new NotImplementedException();
					}
		 }
}
