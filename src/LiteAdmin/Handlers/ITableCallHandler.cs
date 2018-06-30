using System.Threading.Tasks;

namespace LiteAdmin.Handlers
{
    using Core;

    public interface ITableCallHandler : IJsonHandler
    {
        Task Handle(ITable table, string id);
    }
}