namespace LiteAdmin.SqlServer.Columns
{
    using System;
    using Core;

    internal class Column : IColumn
    {
        public Type DataType { get; set; }
        public object DefaultValue { get; set; }
        public string ForeignKey { get; set; }
        public string ForeignTable { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
        public int MaxLength { get; set; }
        public string Name { get; set; }
    }
}
