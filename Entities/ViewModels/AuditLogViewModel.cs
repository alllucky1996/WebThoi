using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class AuditLogViewModel
    {
        public long AccountId { get; set; }

        public DateTime EventDate { get; set; }

        //Cái này dùng để nhóm các bản ghi log vào cùng 1 hành động,
        //ví dụ sửa 5 trường của 1 bản ghi thì sẽ tạo ra 5 bản ghi log, khi đó dùng cột này để nhóm
        public string EventDateDetail { get; set; }


        public string EventType { get; set; }


        public string TableName { get; set; }


        public string RecordKey { get; set; }


        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }

        public string IpAddress { get; set; }
    }
}
