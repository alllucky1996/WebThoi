using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Chức vụ == cấp quản lý
    /// </summary>
    public class CapQuanLy : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập mã cấp quản lý!")]
        [Display(Name = "Mã chức danh")]
        [StringLength(20, ErrorMessage = "Mã chức danh không được vượt quá 20 ký tự!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên cấp quản lý!")]
        [Display(Name = "Tên chức danh")]
        [StringLength(100, ErrorMessage = "Tên chức danh không được vượt quá 100 ký tự!")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(250, ErrorMessage = "Mô tả chức danh không được vượt quá 250 ký tự!")]
        public string Description { get; set; }


        public bool IsDefault { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        // một đơn vị có nhiều chức vụ
        public long? IdDonVi { get; set; }
        [ForeignKey("IdDonVi")]
        public virtual dmDonVi DonVi { get; set; }
        public CapQuanLy()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }

        public string Describe()
        {
            return "{ RoleId : \"" + Id + "\", Name : \"" + Name + "\" }";
        }
    }
}
