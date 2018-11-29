using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models.SystemManage
{
    [Table("AuditLog")]
    public class AuditLog : Entity
    {
        [Key]
        public Guid AuditLogId { get; set; }

        [Required]
        public long AccountId { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        //Cái này dùng để nhóm các bản ghi log vào cùng 1 hành động,
        //ví dụ sửa 5 trường của 1 bản ghi thì sẽ tạo ra 5 bản ghi log, khi đó dùng cột này để nhóm
        public string EventDateDetail { get; set; }

        [Required]
        [MaxLength(1)]//CRUD
        public string EventType { get; set; }

        [Required]
        [MaxLength(100)]
        public string TableName { get; set; }

        [Required]
        public string RecordKey { get; set; }

        [Required]
        [MaxLength(100)]
        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }

        public string IpAddress { get; set; }

        public string Describe()
        {
            return "Logging";
        }
    }
}
