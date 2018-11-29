using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Chức năng thuộc nhóm quyền
    /// </summary>
    [Table("ModuleRole")]
    public class ModuleRole : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long RoleId { get; set; }
        /// <summary>
        /// Mã của chức năng, lấy theo enum
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string ModuleCode { get; set; }
        [Column(TypeName = "tinyint")]
        public byte? Create { get; set; }
        [Column(TypeName = "tinyint")]
        public byte? Read { get; set; }
        [Column(TypeName = "tinyint")]
        public byte? Update { get; set; }
        [Column(TypeName = "tinyint")]
        public byte? Delete { get; set; }
        //Duyệt
        [Column(TypeName = "tinyint")]
        public byte? Verify { get; set; }
        //Xuất bản
        [Column(TypeName = "tinyint")]
        public byte? Publish { get; set; }        

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public string Describe()
        {
            return "{ RoleId : \"" + RoleId + "\", ModuleCode : \"" + ModuleCode + "\" }";
        }
    }
}
