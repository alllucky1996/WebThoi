using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Model
{
	public class unit :Dung
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Đơn vị tính")]
		public string Name { get; set; }

		[Display(Name = "Mô tả")]
		public string Description { get; set; }

		[Display(Name = "Ngày tạo")]
		public DateTime Created_at { get; set; }

		[Display(Name = "Ngày sửa")]
		public DateTime Updated_at { get; set; }
		public bool is_deleted { get; set; }

		public string Describe()
		{
			throw new NotImplementedException();
		}

		public unit()
		{
			Created_at = DateTime.Now;
		}
	}
}
