namespace LiteAdmin.Core
{
    using System;

    public interface IColumn
    {
        Type DataType { get; }
        object DefaultValue { get; }
        string ForeignKey { get; }
        string ForeignTable { get; }
        bool IsPrimaryKey { get; }
        bool IsNullable { get; }
        int MaxLength { get; }
        string Name { get; }
    }
}
