using Entities.Models;
using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
	public class User_KPI : Entity
	{
		[Key]
		public long ID { get; set; }
		public long? IdKPI { get; set; }
		[ForeignKey("IdKPI")]
		public virtual DM_KPI KPIs { get; set; }

		public long? IdUser { get; set; }
		[ForeignKey("IdUser")]
		public virtual Account User { get; set; }

		public long? IdWeight { get; set; }
		[ForeignKey("IdWeight")]
		public virtual Weight Weight { get; set; }

		[Display(Name ="Kết quả")]
		public string result { get; set; }

		public DateTime create_at { get; set; }
		public DateTime? updated_at { get; set; }
		public User_KPI()
		{
			create_at = DateTime.Now;
		}
		public string Describe()
		{
			throw new NotImplementedException();
		}
	}
}
