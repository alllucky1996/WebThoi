namespace Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MauVanBan:Entity
    {
        [Key]
        [Display(Name="Mã")]
        public string Ma { get; set; }
        [Display(Name = "Tên")]
        public string Ten { get; set; }
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        public string Describe()
        {
            return Ten;
        }
    }
}
