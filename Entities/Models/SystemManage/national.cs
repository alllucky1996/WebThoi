﻿using Common.CustomAttributes;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
	[DropDown(ValueField = "Id", TextField = "Name")]
	public class National : Entity
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Dân tộc")]
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

		public National() 
		{
				Created_at = DateTime.Now;
		}
	}
}
