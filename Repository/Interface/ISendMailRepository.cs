using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISendMailRepository
    {
        bool SendMail(string From, string to, string subject, string body, string FromName = "", string cc = "");
    }
}
