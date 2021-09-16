using ViewModels.VM;

namespace Service
{
    public class BaseService
    {

        public CurrentResponse _currentResponse;

        public BaseService()
        {
            _currentResponse = new CurrentResponse();
        }

        //public int LogException(Exception exception)
        //{
        //    return _baseRepository.LogException(exception);
        //}

        public CurrentResponse CreateResponse(object data, int statusCode, string message)
        {
            _currentResponse.data = data;
            _currentResponse.status = statusCode;
            _currentResponse.message = message;

            return _currentResponse;
        }
    }
}
