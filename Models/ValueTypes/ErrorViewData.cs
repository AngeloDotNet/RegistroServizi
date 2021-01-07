using System.Net;

namespace RegistroServizi.Models.ValueTypes
{
    public class ErrorViewData
    {
        public ErrorViewData(string message, string title = "Errore", HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string viewName = "Index")
        {
            Title = title;
            Message = message;
            StatusCode = statusCode;
            ViewName = viewName;
        }

        public string Title { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
        public string ViewName { get; }
    }
}