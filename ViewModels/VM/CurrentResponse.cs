using System.Net;

namespace ViewModels.VM
{
    public class CurrentResponse
    {
        public string Data { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
