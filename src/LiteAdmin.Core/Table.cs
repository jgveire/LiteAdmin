namespace LiteAdmin.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Table : ITable
    {
        public Table()
        {
        }

        public Table(string schema, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        }

        public ICollection<IColumn> Columns { get; } = new List<IColumn>();

        public string Name { get; set; }

        public string PrimaryKey => GetPrimaryKey();

        public string Schema { get; set; }

        private string GetPrimaryKey()
        {
            return Columns.FirstOrDefault(e => e.IsPrimaryKey)?.Name;
        }
    }
}