namespace LiteAdmin.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Columns;
    using Core;

    public class TableRepository : ITableRepository
    {
        public string ConnectionString { get; }

        // "Server=tcp:YourServer,1433;Initial Catalog=YourDatabase;Persist Security Info=True;"
        public TableRepository(string connectionString)
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

                    columnFactory.CreateColumnFromRecord(columnRecord);
                }
            }

            return result;
        }

        private List<ColumnRecord> GetColumnRecords()
        {
            var records = new List<ColumnRecord>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT C.TABLE_SCHEMA, " +
                          "	   C.TABLE_CATALOG, " +
                          "	   C.COLUMN_NAME, " +
                          "	   C.DATA_TYPE, " +
                          "	   CAST(CASE WHEN C.IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS bit) AS IS_NULLABLE, " +
                          "	   C.CHARACTER_MAXIMUM_LENGTH, " +
                          "	   C.COLUMN_DEFAULT, " +
                          "	   CAST(CASE WHEN TC.CONSTRAINT_NAME IS NULL THEN 0 ELSE 1 END AS bit) AS PRIMARY_KEY " +
                          "FROM INFORMATION_SCHEMA.COLUMNS C " +
                          "LEFT JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE CCU ON " +
                          "	   C.TABLE_CATALOG = CCU.TABLE_CATALOG AND " +
                          "	   C.TABLE_SCHEMA = CCU.TABLE_SCHEMA AND " +
                          "	   C.TABLE_NAME = CCU.TABLE_NAME AND " +
                          "	   C.COLUMN_NAME = CCU.COLUMN_NAME " +
                          "LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON " +
                          "	   CCU.TABLE_CATALOG = TC.TABLE_CATALOG AND " +
                          "	   CCU.TABLE_SCHEMA = TC.TABLE_SCHEMA AND " +
                          "	   CCU.TABLE_NAME = TC.TABLE_NAME AND " +
                          "	   CCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME AND " +
                          "	   TC.CONSTRAINT_TYPE = 'PRIMARY KEY'";
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
