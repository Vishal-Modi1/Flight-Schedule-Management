using System;
using ViewModels.VM;

namespace Service.Middleware
{
    public class RestException : Exception
    {
        public RestException(int code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public RestException(CurrentResponse response)
        {
            Code = response.status;
            Errors = response.message;
        }

        public int Code { get; }
        public object Errors { get; }
    }
}
