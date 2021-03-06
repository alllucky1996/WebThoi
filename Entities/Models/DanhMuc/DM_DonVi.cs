﻿using Common.CustomAttributes;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    //[Table("DonVi")]
    [DropDown(ValueField = "Id", TextField = "Name")]
    public class DM_DonVi : Entity
    {
        [Key]
        public long Id { get; set; }
        #region const
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        #endregion
        [StringLength(20, ErrorMessage = "Điện thoại không được vượt quá 20 ký tự!")]
        public string DienThoai { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Cấp đơn vị")]
        public int CapDV { get; set; }

        [Display(Name = "Đơn vị quản lý trực tiếp")]
        public long? IdCha { get; set; }
        [ForeignKey("IdCha")]
        public virtual DM_DonVi DonVi { get; set; }
        public DM_DonVi()
        {
            CreateDate = DateTime.Now;
        }
        //[NotMapped]
        //public int Cap
        //{
        //    get
        //    {
        //        if(IdCha != null)
        //        {
        //            if (DonVi == null)
        //            {
        //                if (DonVi.DonVi == null)
        //                {
        //                    if (DonVi.DonVi.DonVi == null)
        //                    {
        //                        if (DonVi.DonVi.DonVi.DonVi == null)
        //                        {
        //                            // chưa tính để 0
        //                            return 0;
        //                        }
        //                        return 4;
        //                    }
        //                    return 3;
        //                }
        //                return 2;
        //            }
        //            return 1;
        //        }
        //        return 1;
        //    }
        //}
        public string Describe()
        {
            return "{id: " + Id + "}";

        }
    }
}