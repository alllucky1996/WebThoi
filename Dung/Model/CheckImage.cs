using Common.CustomAttributes;
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
    [Table("CheckImage")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class CheckImage : Entity
	{
        [Key]
        public long Id { get; set; }
        [Display(Name ="Mã")]
        public string Code { get; set; }
        #region const
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string HasError { get; set; }
        public bool? IsDeleted { get; set; }
        #endregion
        [Display(Name="Tên hình ảnh")]
        public string Name { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        public CheckImage()
        {
            CreateDate = DateTime.Now;
        }
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
}
