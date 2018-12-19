using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Model
{
	public class User_KPI : Dung
	{
		[Key]
		public int ID { get; set; }
		public int? IdKPI { get; set; }
		[ForeignKey("IdKPI")]
		public virtual dmKPI KPIs { get; set; }

		public long? IdUser { get; set; }
		[ForeignKey("IdUser")]
		public virtual Account User { get; set; }
		public string Describe()
		{
			throw new NotImplementedException();
		}
	}
}
