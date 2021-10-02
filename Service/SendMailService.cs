using Configuration;
using DataModels.Constants;
using DataModels.Models;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VM;

namespace Service
{
    public class SendMailService : BaseService,ISendMailService
    {
		private readonly IConfiguration _config;
		private readonly ISendMailRepository _sendMailRepository;
		private readonly IUserRepository _userRepository;

		public SendMailService(IConfiguration config, ISendMailRepository sendMailRepository, IUserRepository userRepository)
		{
			_config = config;
			_sendMailRepository = sendMailRepository;
            _userRepository = userRepository;

        }

        public bool SendCreateUserMail(UserVM userVM)
        {
			try
			{
				string FromEmail = _config.GetValue<string>("MailSetting");

				var UserRegistrationMail = GetEmailTemplate(Constants.UserSignInTemplate);
				UserRegistrationMail = UserRegistrationMail.Replace("{name}", userVM.FirstName + " " + userVM.LastName);
				UserRegistrationMail = UserRegistrationMail.Replace("{username}", userVM.Email);
				UserRegistrationMail = UserRegistrationMail.Replace("{password}", userVM.Password);
				_sendMailRepository.SendMail(FromEmail, userVM.Email, "Sign In" , UserRegistrationMail, "","");

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

        public CurrentResponse PasswordReset(string Email, string Url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    var user = _userRepository.IsEmailExist(Email);
                    if (user)
                    {
                        var Mail = GetEmailTemplate(Constants.ForgotPasswordTemplate);
                        Mail = Mail.Replace("{Link}", Url);
                        _sendMailRepository.SendMail(_config.GetValue<string>("MailSetting"), Email, "Password Reset", Mail, "", "");
                       
                        CreateResponse(user, HttpStatusCode.OK, "Mail sent successfully");
                        return _currentResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                CreateResponse(ex, HttpStatusCode.InternalServerError, ex.Message);
                return _currentResponse;
            }
            return _currentResponse;
        }

        public string GetEmailTemplate(string templateName)
        {
            try
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates",templateName);
                return System.IO.File.ReadAllText(file);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
