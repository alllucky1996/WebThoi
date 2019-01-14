using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
	public class Weight : Entity
	{
		[Key]
		public long id { get; set; }

		[Required]
		public int code { get; set; }

		public DateTime created_at { get; set; }

		public DateTime? updated_at { get; set; }

		public Boolean? is_delete { get; set; }

		public Weight()
		{
			created_at = DateTime.Now;
		}

					public string Describe()
					{
							 return "{ Weight: \"" + id;
					}
		 }
}
