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
    public long Id { get; set; }
    [Display(Name ="Mã")]
    public string Code { get; set; }
    #region const
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    [Display(Name="Tên")]
    public string Name { get; set; }
    #endregion

    [Display(Name = "Trọng số (%)")]
    public int TrongSo { get; set; }

    [Display(Name = "Trọng số chỉ tiêu (%)")]
    public int? TrongSoChiTieu { get; set; }

    public long? IdCha { get; set; }
    [ForeignKey("IdCha")]
    public virtual dmKPI DmKPI { get; set; }
    public long? IdDonVi { get; set; }
    [ForeignKey("IdDonVi")]
    public virtual dmDonVi DonVi { get; set; }

		[ForeignKey("Id")]
		public virtual unit Unit { get; set; }

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
