using Common.CustomAttributes;
using Dung.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dung.Model
{
  //  [Table("dmKPI")]
  [DropDown(ValueField = "Id", TextField = "Name")]
  public class dmKPI : Dung
  {
    [Key]
    public int Id { get; set; }
    [Display(Name ="Mã")]
    public string Code { get; set; }
    #region const
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
		#endregion

		[Display(Name = "Tên")]
		public string Name { get; set; }

    public int? IdCha { get; set; }
    [ForeignKey("IdCha")]
    public virtual dmKPI DmKPI { get; set; }

		public int? IdUnit { get; set; }
		[ForeignKey("IdUnit")]
		public virtual unit Unit { get; set; }

		public  int? IdWeight { get; set; }
		[ForeignKey("IdWeight")]
		public virtual Weight Weight { get; set; }
		public dmKPI()
    {
      CreateDate = DateTime.Now;
    }
    public string Describe()
    {
      return "{id: " + Id + "}";
    }
  }
}
