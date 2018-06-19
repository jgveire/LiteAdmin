using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiteAdmin.Handlers
{
    public interface IStaticFileHandler
    {
        Task Handle(HttpContext context, PathString remainingPath);
    }
}