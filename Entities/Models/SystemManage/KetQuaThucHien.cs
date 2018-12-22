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
	 // [Table("KetQuaThucHien")]
[DropDown(ValueField = "Id", TextField = "Name")]
    public class KetQuaThucHien : Entity
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

        [Display(Name = "Người chấm ")]
        // get người chấm là người truy cập hiện tại
        public int IdNguoiCham { get; set; }

        [Display(Name = "Cá nhân tự chấm ")]
        // get người chấm là người truy cập hiện tại
        public bool CaNhanTuCham { get; set; }
        
        public KetQuaThucHien()
        {
            CreateDate = DateTime.Now;
           
        }
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
}
