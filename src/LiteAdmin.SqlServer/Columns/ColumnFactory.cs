namespace LiteAdmin.SqlServer.Columns
{
    using System;
    using System.Collections.Generic;
    using Core;

    internal class ColumnFactory
    {
        private readonly Dictionary<Type, Type> _columnDictionary = InitDictionary();

        public IColumn CreateColumnFromRecord(ColumnRecord columnRecord)
        {
            if (columnRecord == null)
            {
                throw new ArgumentNullException(nameof(columnRecord));
            }
            else if (!_columnDictionary.ContainsKey(columnRecord.DataType))
            {
                throw new NotSupportedException($"The column type {columnRecord.DataType?.FullName} is not supported.");
            }

            var type = _columnDictionary[columnRecord.DataType];
            var columnName = char.ToLowerInvariant(columnRecord.ColumnName[0]) + columnRecord.ColumnName.Substring(1);
            var args = new object[] { columnName, columnRecord.IsNullable, columnRecord.MaximumLength, columnRecord.DefaultValue, columnRecord.IsPrimaryKey };
            IColumn column = (IColumn)Activator.CreateInstance(type, args);
            return column;
        }

        private static Dictionary<Type, Type> InitDictionary()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(byte), typeof(ByteColumn) },
                { typeof(char), typeof(CharColumn) },
                { typeof(DateTime), typeof(DateTimeColumn) },
                { typeof(decimal), typeof(DecimalColumn) },
                { typeof(double), typeof(DoubleColumn) },
                { typeof(float), typeof(FloatColumn) },
                { typeof(Int16), typeof(Int16Column) },
                { typeof(Int32), typeof(Int32Column) },
                { typeof(Int64), typeof(Int64Column) },
                { typeof(sbyte), typeof(SByteColumn) },
                { typeof(string), typeof(StringColumn) },
                { typeof(Guid), typeof(GuidColumn) },
            };
        }
    }
}
