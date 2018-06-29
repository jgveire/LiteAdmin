namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Columns;
    using Core;
    using Sql;

    public class SchemaRepository : ISchemaRepository
    {
        public string ConnectionString { get; }

        public SchemaRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public ICollection<ITable> GetTables()
        {
            var records = GetColumnRecords();
            return GetTables(records);
        }

        private static ICollection<ITable> GetTables(List<ColumnRecord> records)
        {
            var columnFactory = new ColumnFactory();
            var result = new List<ITable>();
            var groupedRecords = records.GroupBy(e => new { e.TableSchema, e.TableName });
            foreach (var groupedRecord in groupedRecords)
            {
                var table = new Table(groupedRecord.Key.TableSchema, groupedRecord.Key.TableName);
                foreach (var columnRecord in groupedRecord)
                {
                    var column = columnFactory.CreateColumnFromRecord(columnRecord);
                    table.Columns.Add(column);
                }
                result.Add(table);
            }

            return result;
        }

        private List<ColumnRecord> GetColumnRecords()
        {
            var records = new List<ColumnRecord>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = SqlScript.Schema;
                var command = new SqlCommand(sql, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = new ColumnRecord(
                            reader[0] as string,
                            reader[1] as string,
                            reader[2] as string,
                            reader[3] as string,
                            (bool)reader[4],
                            reader[5] as int? ?? 0,
                            reader[6] as string,
                            reader[7] as bool? ?? false);
                        records.Add(record);
                    }
                }
            }

            return records;
        }
    }
}
