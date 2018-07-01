namespace LiteAdmin.Models
{
    public class ColumnModel
    {
        public string DataType { get; set; }

        public string DefaultValue { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int MaxLength { get; set; }

        public string Name { get; set; }
    }
}