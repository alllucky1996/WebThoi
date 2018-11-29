using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ThongBao : Entity
    {
        [Key]
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public string Email { get; set; }
        public string Ma { get; set; }
        public DateTime ThoiGianGui {get;set;}
        public DateTime? ThoiGianXem { get; set; }
        public bool DaXem { get; set; }
        public bool CoGuiEmail { get; set; }
        public bool DaGuiEmail { get; set; }
        public string TieuDe { get; set; }
        public int MucDoQuanTrong { get; set; }
        public string NguoiGui { get; set; }
        public bool An { get; set; }
        public string NoiDung { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account AccountNguoiNhan { get; set; }
        public string Describe()
        {
            return "";
        }
    }
}
