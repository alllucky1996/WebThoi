using Common.CustomAttributes;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.SystemManage
{
  //  [Table("dmKPI")]
  [DropDown(ValueField = "Id", TextField = "Name")]
  public class dmKPI : Entity
	{
    [Key]
    public long Id { get; set; }
    [Display(Name ="Mã")]
    public string Code { get; set; }

    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

		[Display(Name = "Tên")]
		public string Name { get; set; }

    public long? IdCha { get; set; }
    [ForeignKey("IdCha")]
    public virtual dmKPI DmKPI { get; set; }

		public int? IdUnit { get; set; }
		[ForeignKey("IdUnit")]
		public virtual unit Unit { get; set; }

		public dmKPI()
    {
      CreateDate = DateTime.Now;
			IsDeleted = false;
    }
    public string Describe()
    {
      return "{id: " + Id + "}";
    }
  }
}
