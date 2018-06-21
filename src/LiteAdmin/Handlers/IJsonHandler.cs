using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LiteAdmin.Handlers
{
    public interface IJsonHandler
    {
        HttpContext Context { get; set; }
        HttpRequest Request { get; }
        HttpResponse Response { get; }
        JsonSerializerSettings SerializerSettings { get; }
    }
}