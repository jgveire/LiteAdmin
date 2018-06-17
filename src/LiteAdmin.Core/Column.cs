namespace LiteAdmin.Core
{
    using System;

    public class Column : IColumn
    {
        public Type DataType { get; set; }
        public object DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public int MaxLength { get; set; }
        public string Name { get; set; }
    }
}