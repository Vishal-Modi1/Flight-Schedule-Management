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
        public bool SendMail(string From, string to, string subject, string body, string FromName = "", string cc = "")
        {
            //NEEED TO REMOVE AND ALSO NEED TO FETCH 
            From = "hardikmasalawala88@gmail.com";
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(From, "8866mom143");
                smtp.EnableSsl = true;

                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(new MailAddress(to));
                myMessage.From = new MailAddress(From);
                myMessage.Body = body;
                myMessage.IsBodyHtml = true;
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
