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
        public DatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }

        public async Task DeleteItemAsync(string tableName, string id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"DELETE FROM {tableName} WHERE Id = @Identifier";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Identifier", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Dictionary<string, object>> GetItemAsync(string tableName, string id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"SELECT * FROM {tableName} WHERE Id = @Identifier";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Identifier", id);
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var schema = reader.GetColumnSchema();
                    if (await reader.ReadAsync())
                    {
                        var record = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            record.Add(schema[i].ColumnName, reader[i]);
                        }

                        return record;
                    }
                }
            }

            return null;
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

        public async Task UpdateItemAsync(string tableName, string id, Dictionary<string, object> dictionary)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"UPDATE {tableName} SET ";
                foreach (var item in dictionary)
                {
                    sql += $"{item.Key} = @{item.Key},";
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += " WHERE Id = @Identifier";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Identifier", id);
                foreach (var item in dictionary)
                {
                    command.Parameters.AddWithValue($"@{item.Key}", item.Value);
                }

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task InsertItemAsync(string tableName, Dictionary<string, object> dictionary)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"INSERT INTO {tableName} (";
                foreach (var item in dictionary)
                {
                    sql += $"{item.Key},";
                }

                sql = sql.Substring(0, sql.Length - 1) + ") VALUES (";
                foreach (var item in dictionary)
                {
                    sql += $"@{item.Key},";
                }
                sql = sql.Substring(0, sql.Length - 1) + ")";

                var command = new SqlCommand(sql, connection);
                foreach (var item in dictionary)
                {
                    command.Parameters.AddWithValue($"@{item.Key}", item.Value);
                }

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
