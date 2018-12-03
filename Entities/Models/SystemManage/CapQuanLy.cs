using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Nhóm quyền
    /// </summary>
    public class CapQuanLy : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã cấp quản lý!")]
        [Display(Name = "Mã cấp quản lý")]
        [StringLength(20, ErrorMessage = "Mã cấp quản lý không được vượt quá 20 ký tự!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên cấp quản lý!")]
        [Display(Name = "Tên cấp quản lý")]
        [StringLength(100, ErrorMessage = "Tên cấp quản lý không được vượt quá 100 ký tự!")]
        public string Name { get; set; }

        [Display(Name = "Mô tả!")]
        [StringLength(250, ErrorMessage = "Mô tả cấp quản lý không được vượt quá 250 ký tự!")]
        public string Description { get; set; }


        public bool IsDefault { get; set; }

        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public CapQuanLy()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
        #endregion
        public string Describe()
        {
            return "{ RoleId : \"" + Id + "\", Name : \"" + Name + "\" }";
        }
    }
}
