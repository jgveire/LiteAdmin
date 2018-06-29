namespace LiteAdmin.SqlServer.Columns
{
    using System;
    using Core;

    internal abstract class ColumnBase<TData> : IColumn
    {
        protected ColumnBase(string name, bool isNullable, int maxLength, bool isPrimaryKey)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            Name = name;
            IsNullable = isNullable;
            MaxLength = maxLength;
            IsPrimaryKey = isPrimaryKey;
        }

        public Type DataType { get; } = typeof(TData);
        public abstract object DefaultValue { get; }
        public bool IsPrimaryKey { get; }
        public bool IsNullable { get; }
        public int MaxLength { get; }
        public string Name { get; }
    }
}
