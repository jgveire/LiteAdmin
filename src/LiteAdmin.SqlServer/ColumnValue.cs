namespace LiteAdmin.SqlServer
{
    using Core;

    public class ColumnValue : IColumnValue
    {
        public ColumnValue(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get; }
    }
}
