using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    /// <summary>
    /// Account thuộc nhóm quyền nào
    /// </summary>
    [Table("AccountRole")]
    public class AccountRole : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //Người dùng
        public long AccountId { get; set; }
        //Nhóm quyền
        public long RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string Describe()
        {
            return "{ AccountId : \"" + AccountId + "\", RoleId : \"" + RoleId + "\" }";
        }
    }
}
