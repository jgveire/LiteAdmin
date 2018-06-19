namespace LiteAdmin.Handlers
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Constants;
    using Microsoft.AspNetCore.Http;

    public class ApiCallHandler : HandlerBase, IApiCallHandler
    {
        public Task Handle(HttpContext context, PathString remainingPath)
        {
            var statusCode = (int)HttpStatusCode.NotFound;
            var json = GetErrorJson(JsonErrorCode.MethodNotFound, JsonErrorMessage.MethodNotFound);
            var bytes = Encoding.UTF8.GetBytes(json);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = ContentTypeProvider.Mappings[".json"];
            return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
