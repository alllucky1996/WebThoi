using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Model
{
	class user : Entities.Models.SystemManage.Account
	{
		[ForeignKey("Id")]
		public virtual national National { get; set; }
	}
}
