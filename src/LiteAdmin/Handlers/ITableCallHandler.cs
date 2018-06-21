using System.Threading.Tasks;

namespace LiteAdmin.Handlers
{
    public interface ITableCallHandler : IJsonHandler
    {
        Task Handle(string tableName);
    }
}