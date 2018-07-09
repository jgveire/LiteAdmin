namespace LiteAdmin.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDatabaseRepository
    {
        Task DeleteItemAsync(ITable table, string id);
        Task<Dictionary<string, object>> GetItemAsync(ITable table, string id);
        Task<ICollection<Dictionary<string, object>>> GetItemsAsync(ITable table, int pageNumber = 1, int pageSize = 20);
        Task<IEnumerable<LookupModel>> GetLookupItems(ITable table);
        Task InsertItemAsync(ITable table, Dictionary<string, object> dictionary);
        Task UpdateItemAsync(ITable table, string id, Dictionary<string, object> dictionary);
    }
}
