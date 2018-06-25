namespace LiteAdmin.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDatabaseRepository
    {
        Task DeleteItemAsync(string tableName, string id);
        Task<Dictionary<string, object>> GetItemAsync(string tableName, string id);
        Task<ICollection<Dictionary<string, object>>> GetItemsAsync(string tableName, int pageNumber, int pageSize = 20);
        Task InsertItemAsync(string tableName, Dictionary<string, object> dictionary);
        Task UpdateItemAsync(string tableName, string id, Dictionary<string, object> dictionary);
    }
}
