namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Core;
    using Sql;
    using Column = Columns.Column;

    public class SchemaRepository : ISchemaRepository
    {
        private readonly DataTypeDictionary _dataTypes = new DataTypeDictionary();

        public SchemaRepository(string connectionString, IEnumerable<string> tables)
        {
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }

        public IEnumerable<string> Tables { get; }

        public ICollection<ITable> GetTables()
        {
            var records = GetColumnRecords();
            return GetTables(records);
        }

        private ICollection<ITable> GetTables(List<ColumnRecord> records)
        {
            var result = new List<ITable>();
            var groupedRecords = records.GroupBy(e => new { e.TableSchema, e.TableName });
            foreach (var groupedRecord in groupedRecords)
            {
                if (!Tables.Contains(groupedRecord.Key.TableName, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                var table = new Table(groupedRecord.Key.TableSchema, groupedRecord.Key.TableName);
                foreach (var columnRecord in groupedRecord)
                {
                    var column = new Column
                    {
                        IsPrimaryKey = columnRecord.IsPrimaryKey,
                        Name = columnRecord.ColumnName,
                        IsNullable = columnRecord.IsNullable,
                        DefaultValue = null,
                        MaxLength = columnRecord.MaximumLength,
                        DataType = columnRecord.DataType,
                        ForeignKey = columnRecord.ForeignKey,
                        ForeignTable = columnRecord.ForeignTable
                    };
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
                        var value = reader[3] as string;
                        var dataType = GetDataType(value);
                        if (dataType == null)
                        {
                            continue;
                        }

                        var record = new ColumnRecord { 
                            TableSchema = ToCamelCase(reader[0] as string),
                            TableName = ToCamelCase(reader[1] as string),
                            ColumnName = ToCamelCase(reader[2] as string),
                            DataType = dataType,
                            IsNullable = (bool)reader[4],
                            MaximumLength = reader[5] as int? ?? 0,
                            DefaultValue = reader[6] as string,
                            IsPrimaryKey = reader[7] as bool? ?? false,
                            ForeignTable = ToCamelCase(reader[8] as string),
                            ForeignKey = ToCamelCase(reader[9] as string)
                        };
                        records.Add(record);
                    }
                }
            }

            return records;
        }

        private string ToCamelCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return value[0].ToString().ToLower() + value.Substring(1, value.Length - 1);
        }

        private Type GetDataType(string value)
        {
            if (!_dataTypes.ContainsKey(value))
            {
                return null;
            }

            return _dataTypes[value];
        }
    }
}








