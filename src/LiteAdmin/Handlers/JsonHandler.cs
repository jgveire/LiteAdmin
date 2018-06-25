namespace LiteAdmin.Handlers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonHandler : HandlerBase, IJsonHandler
    {
        public HttpContext Context { get; set; }

        public HttpRequest Request => Context?.Request;

        public HttpResponse Response => Context?.Response;

        public JsonSerializerSettings SerializerSettings { get; } = CreateSerializerSettings();

        protected Task HttpNotFoundResponse()
        {
            var statusCode = (int)HttpStatusCode.NotFound;
            var json = GetErrorJson(JsonErrorCode.MethodNotFound, JsonErrorMessage.MethodNotFound);
            var bytes = Encoding.UTF8.GetBytes(json);
            Response.StatusCode = statusCode;
            Response.ContentType = ContentTypeProvider.Mappings[".json"];
            return Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        protected void HttpNoContentResponse()
        {
            var statusCode = (int)HttpStatusCode.NoContent;
            Response.StatusCode = statusCode;
        }

        protected Task JsonResponse(object data)
        {
            var statusCode = (int)HttpStatusCode.OK;
            string json = JsonConvert.SerializeObject(data, SerializerSettings);
            var bytes = Encoding.UTF8.GetBytes(json);
            Response.StatusCode = statusCode;
            Response.ContentType = ContentTypeProvider.Mappings[".json"];
            return Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        protected Dictionary<string, object> DeserializeJson(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json, SerializerSettings);
        }

        private static JsonSerializerSettings CreateSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            };
        }
    }
}