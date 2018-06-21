namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Core;
    public class DatabaseRepository : IDatabaseRepository
    {
        public string ConnectionString { get; }

        public DatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public async Task<ICollection<Dictionary<string, object>>> GetItemsAsync(string tableName, int pageNumber, int pageSize = 20)
        {
            var records = new List<Dictionary<string, object>>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"SELECT * FROM {tableName} " +
                          "ORDER BY Id " +
                          "OFFSET @PageSize * (@PageNumber - 1) ROWS " +
                          "FETCH NEXT @PageSize ROWS ONLY";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@PageNumber", pageNumber);
                command.Parameters.AddWithValue("@PageSize", pageSize);
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var schema = reader.GetColumnSchema();
                    while (await reader.ReadAsync())
                    {
                        var record = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            record.Add(schema[i].ColumnName, reader[i]);
                        }

                        records.Add(record);
                    }
                }
            }

            return records;
        }
    }
}
