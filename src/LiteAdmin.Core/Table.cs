namespace LiteAdmin.Core
{
    using System;
    using System.Collections.Generic;

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

        public string Name { get; set; }

        public string Schema { get; set; }

        public ICollection<IColumn> Columns { get; } = new List<IColumn>();
    }
}