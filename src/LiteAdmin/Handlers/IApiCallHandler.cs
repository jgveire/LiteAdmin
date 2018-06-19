using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiteAdmin.Handlers
{
    public interface IApiCallHandler
    {
        Task Handle(HttpContext context, PathString remainingPath);
    }
}