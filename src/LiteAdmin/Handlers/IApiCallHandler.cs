using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LiteAdmin.Handlers
{
    public interface IApiCallHandler : IJsonHandler
    {
        Task Handle(PathString remainingPath);
    }
}