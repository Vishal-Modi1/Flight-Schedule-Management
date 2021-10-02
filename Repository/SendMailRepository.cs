using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SendMailRepository : ISendMailRepository
    {
        public bool SendMail(string host, int port, string from, string fromPaasKey, string to, string subject, string body, string FromName = "", string cc = "", bool isBodyHtml = true, bool isEnableSsl = true)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.Port = port;
                smtp.Credentials = new System.Net.NetworkCredential(from, fromPaasKey);
                smtp.EnableSsl = isEnableSsl;

                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(new MailAddress(to));
                myMessage.From = new MailAddress(from);
                myMessage.Body = body;
                myMessage.IsBodyHtml = isBodyHtml;
                if (!string.IsNullOrEmpty(cc))
                    myMessage.CC.Add(cc);
                myMessage.Subject = subject;
                smtp.Send(myMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
