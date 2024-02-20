using System.Net;

namespace Lab5.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T> Data { get; set; }
    }
}
