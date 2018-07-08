namespace LiteAdmin.SqlServer
{
    using System;

    internal class ColumnRecord
    {
        private readonly string _dataType;
        private readonly DataTypeDictionary _dataTypes = new DataTypeDictionary();

        public ColumnRecord()
        {
        }

        public ColumnRecord(
            string tableSchema,
            string tableName,
            string columnName,
            string dataType,
            bool isNullable,
            int maximumLength,
            string defaultValue,
            bool isPrimaryKey)
        {
            _dataType = dataType;
            TableSchema = tableSchema ?? throw new ArgumentNullException(nameof(tableSchema));
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            DataType = ConvertToType(dataType);
            IsNullable = isNullable;
            MaximumLength = maximumLength;
            DefaultValue = defaultValue;
            IsPrimaryKey = isPrimaryKey;
        }

        public string ColumnName { get; set; }

        public Type DataType { get; set; }

        public string DefaultValue { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int MaximumLength { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }

        public string ForeignKey { get; set; }

        public string ForeignTable { get; set; }

        private Type ConvertToType(string dataType)
        {
            return _dataTypes[dataType];
        }
    }
}
