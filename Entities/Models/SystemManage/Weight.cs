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
		public int id { get; set; }

		[Required]
		public int code { get; set; }

		public DateTime created_at { get; set; }

		public Boolean? is_delete { get; set; }
		public string Describe()
		{
			throw new NotImplementedException();
		}

		public Weight()
		{
			created_at = DateTime.Now;
		}
	}
}
