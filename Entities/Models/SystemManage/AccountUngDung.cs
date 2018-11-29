using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models.SystemManage
{
    [Table("AccountUngDung")]
    public class AccountUngDung : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //Người dùng
        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string Describe()
        {
            return "{ AccountId : \"" + AccountId;
        }
    }
}
