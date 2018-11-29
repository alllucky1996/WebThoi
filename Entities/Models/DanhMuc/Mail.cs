using Entities.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public class Mail
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Mail người gửi")]
        public string MailNguoiGui { get; set; }

        [Display(Name = "Mail người gửi")]
        public string MailNguoiNhan { get; set; }
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "Nội dung")]
        public string Body { get; set; }

        public string description()
        {
            return "Gửi mail cho " + MailNguoiNhan + "\"" + " tiêu đề : " + Subject + "\"" + " với nội dung " + Body;
        }
    }
}
