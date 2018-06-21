namespace LiteAdmin.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDatabaseRepository
    {
        Task<ICollection<Dictionary<string, object>>> GetItemsAsync(string tableName, int pageNumber, int pageSize = 20);
    }
}
