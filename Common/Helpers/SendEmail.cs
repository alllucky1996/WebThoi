using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class SendEmail
    {
        private static readonly string SmtpClientAddress_Gmail = ConfigurationManager.AppSettings["SmtpClientAddress_Gmail"].ToString();
        private static readonly string SmtpClientAddress_VNUmail = ConfigurationManager.AppSettings["SmtpClientAddress_VNUmail"].ToString();

        private static readonly string SmtpClientPost_Gmail = ConfigurationManager.AppSettings["SmtpClientPost_Gmail"].ToString();
        private static readonly string SmtpClientPost_VNUmail = ConfigurationManager.AppSettings["SmtpClientPost_VNUmail"].ToString();

        public static bool Send(string displayName, string sender, string senderPassword, string receiver, string subject, string content)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                var client = new SmtpClient(sender.EndsWith("vnu.edu.vn") ? SmtpClientAddress_VNUmail : SmtpClientAddress_Gmail, Convert.ToInt32(sender.EndsWith("vnu.edu.vn") ? SmtpClientPost_VNUmail : SmtpClientPost_Gmail))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Split('@')[0], senderPassword),
                    EnableSsl = true
                };
                if (sender.EndsWith("vnu.edu.vn"))
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors
    sslPolicyErrors)
                    { return true; };
                mail.From = new MailAddress(sender, displayName);
                mail.To.Add(StringHelper.KillChars(receiver));
                mail.Subject = subject;
                mail.Body = content;
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Gửi mail có đính kèm
        public static bool Send(string displayName, string sender, string senderPassword, string receiver, string subject, string content, List<string> att)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                var client = new SmtpClient(sender.EndsWith("vnu.edu.vn") ? SmtpClientAddress_VNUmail : SmtpClientAddress_Gmail, Convert.ToInt32(sender.EndsWith("vnu.edu.vn") ? SmtpClientPost_VNUmail : SmtpClientPost_Gmail))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Split('@')[0], senderPassword),
                    EnableSsl = true
                };
                if (sender.EndsWith("vnu.edu.vn"))
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors
    sslPolicyErrors)
                    { return true; };
                mail.From = new MailAddress(sender, displayName);
                mail.To.Add(StringHelper.KillChars(receiver));
                mail.Subject = subject;
                mail.Body = content;
                if (att.Any())
                {
                    foreach (var file in att)
                    {
                        if (System.IO.File.Exists(file))
                        {

                            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                            ContentDisposition disposition = data.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(file);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                            mail.Attachments.Add(data);
                        }
                    }
                }
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> SendAsync(string displayName, string sender, string senderPassword, string receiver, string subject, string content)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                var client = new SmtpClient(sender.EndsWith("vnu.edu.vn") ? SmtpClientAddress_VNUmail : SmtpClientAddress_Gmail, Convert.ToInt32(sender.EndsWith("vnu.edu.vn") ? SmtpClientPost_VNUmail : SmtpClientPost_Gmail))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Split('@')[0], senderPassword),
                    EnableSsl = true
                };
                if (sender.EndsWith("vnu.edu.vn"))
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors
    sslPolicyErrors)
                    { return true; };
                mail.From = new MailAddress(sender, displayName);
                mail.To.Add(StringHelper.KillChars(receiver));
                mail.Subject = subject;
                mail.Body = content;
                await client.SendMailAsync(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Gửi mail có đính kèm
        public static async Task<bool> SendAsync(string displayName, string sender, string senderPassword, string receiver, string subject, string content, List<string> att)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                var client = new SmtpClient(sender.EndsWith("vnu.edu.vn") ? SmtpClientAddress_VNUmail : SmtpClientAddress_Gmail, Convert.ToInt32(sender.EndsWith("vnu.edu.vn") ? SmtpClientPost_VNUmail : SmtpClientPost_Gmail))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Split('@')[0], senderPassword),
                    EnableSsl = true
                };
                if (sender.EndsWith("vnu.edu.vn"))
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors
    sslPolicyErrors)
                    { return true; };
                mail.From = new MailAddress(sender, displayName);
                mail.To.Add(StringHelper.KillChars(receiver));
                mail.Subject = subject;
                mail.Body = content;
                if (att.Any())
                {
                    foreach (var file in att)
                    {
                        if (System.IO.File.Exists(file))
                        {

                            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                            ContentDisposition disposition = data.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(file);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                            mail.Attachments.Add(data);
                        }
                    }
                }
                await client.SendMailAsync(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Email GetEmailFromConfig()
        {
            try
            {
                var e = new Email();
                e.SmtpClientAddress = ConfigurationManager.AppSettings["SmtpClientAddress"].ToString();

                int port = -1;
                if (int.TryParse(ConfigurationManager.AppSettings["SmtpClientPost"].ToString(), out port))
                    e.SmtpClientPort = port;
                else e.SmtpClientPort = -1;
                e.EmailAddress = ConfigurationManager.AppSettings["EmailAddress"].ToString();
                e.EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();
                e.Name = ConfigurationManager.AppSettings["EmailName"].ToString();
                return e;
            }
            catch
            {
                return new Email();
            }
        }
        public static bool Send(Email email, string toEmailAddress, string subject, string content)
        {
            if (email == null) return false;
            try
            {

                return (Send(email.Name, email.SmtpClientAddress, email.SmtpClientPort.ToString(), email.EmailAddress, email.EmailPassword, toEmailAddress, subject, content));
            }
            catch
            {
                return false;
            }

        }
        public static bool Send(string displayName, string smtpClientAddress, string smtpClientPost, string emailAddress, string emailPassword, string toEmail, string emailSubject, string emailContent)
        {
            //Start Gửi mail
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                var client = new SmtpClient(smtpClientAddress, Convert.ToInt32(smtpClientPost))
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailAddress.Split('@')[0], emailPassword),
                    EnableSsl = true
                };
                if (emailAddress.EndsWith("vnu.edu.vn"))
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors
    sslPolicyErrors)
                    { return true; };
                mail.From = new MailAddress(emailAddress, displayName);
                mail.To.Add(StringHelper.KillChars(toEmail));
                mail.Subject = emailSubject;
                mail.Body = emailContent;
                client.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
            //End gửi mail
        }
    }
    public class Email
    {
        public long Id { get; set; }
        public string Name { get; set; } //Địa chỉ Smtp của dịch vụ email
        public string SmtpClientAddress { get; set; } //Địa chỉ Smtp của dịch vụ email
        public int? SmtpClientPort { get; set; } //Cổng kết nối của dịch vụ email
        public string EmailAddress { get; set; } //Địa chỉ email của hệ thống, dùng để gửi mail cho người dùng 
        public string EmailPassword { get; set; } //Mật khẩu của địa chỉ email trên
    }
}
