using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISendMailRepository
    {
        bool SendMail(string host, int port, string from, string fromPaasKey, string to, string subject, string body, string FromName = "", string cc = "", bool isBodyHtml = true, bool isEnableSsl = true);
    }
}
