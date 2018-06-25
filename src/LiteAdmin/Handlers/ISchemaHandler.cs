namespace LiteAdmin.Handlers
{
    using System.Threading.Tasks;

    public interface ISchemaHandler : IJsonHandler
    {
        Task Handle();
    }
}