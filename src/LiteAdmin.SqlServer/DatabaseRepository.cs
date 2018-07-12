namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    public class DatabaseRepository : IDatabaseRepository
    {
        public DatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }

        public async Task DeleteItemAsync(ITable table, string id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"DELETE FROM {table.Name} WHERE {table.PrimaryKey} = @Identifier";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Identifier", id);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Dictionary<string, object>> GetItemAsync(ITable table, string id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"SELECT * FROM {table.Name} WHERE {table.PrimaryKey} = @Identifier";
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

        public async Task<ICollection<Dictionary<string, object>>> GetItemsAsync(
            ITable table,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var records = new List<Dictionary<string, object>>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"SELECT * FROM {table.Name} " +
                          $"ORDER BY {table.PrimaryKey} " +
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

        public async Task<IEnumerable<LookupModel>> GetLookupItems(ITable table)
        {
            var items = new List<LookupModel>();
            var idColumn = GetIdColumnName(table);
            var nameColumn = GetNameColumnName(table);
            if (idColumn == null)
            {
                throw new InvalidOperationException($"The table {table.Name} does not have a primary key.");
            }

            string sql = $"SELECT {idColumn} AS Id, {nameColumn} AS Name FROM {table.Name} ORDER BY 2";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lookup = new LookupModel
                        {
                            Id = reader["Id"],
                            Name = reader["Name"]?.ToString()
                        };
                        items.Add(lookup);
                    }
                }
            }

            return items;
        }

        public async Task InsertItemAsync(ITable table, Dictionary<string, object> dictionary)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"INSERT INTO {table.Name} (";
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
                    var value = GetDatabaseValue(item);
                    command.Parameters.AddWithValue($"@{item.Key}", value);
                }

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        private static object GetDatabaseValue(KeyValuePair<string, object> item)
        {
            object value = item.Value;
            if (value == null || value.ToString() == "")
            {
                value = DBNull.Value;
            }

            return value;
        }

        public async Task UpdateItemAsync(ITable table, string id, Dictionary<string, object> dictionary)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = $"UPDATE {table.Name} SET ";
                foreach (var item in dictionary)
                {
                    sql += $"{item.Key} = @{item.Key},";
                }

                sql = sql.Substring(0, sql.Length - 1);
                sql += $" WHERE {table.PrimaryKey} = @Identifier";

                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Identifier", id);
                foreach (var item in dictionary)
                {
                    var value = GetDatabaseValue(item);
                    command.Parameters.AddWithValue($"@{item.Key}", value);
                }

                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        private static string GetIdColumnName(ITable table)
        {
            foreach (IColumn column in table.Columns)
            {
                if (column.IsPrimaryKey)
                {
                    return column.Name;
                }
            }

            return null;
        }

        private string GetNameColumnName(ITable table)
        {
            foreach (var column in table.Columns)
            {
                if (!column.IsPrimaryKey && column.DataType == typeof(string))
                {
                    return column.Name;
                }
            }

            foreach (var column in table.Columns)
            {
                if (!column.IsPrimaryKey)
                {
                    return column.Name;
                }
            }

            return table.Columns.First().Name;
        }
    }
}
