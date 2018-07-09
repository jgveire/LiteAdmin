namespace LiteAdmin.Handlers
{
    using System.Threading.Tasks;
    using Core;

    public interface ILookupCallHandler : IJsonHandler
    {
        Task Handle(ITable table);
    }
}