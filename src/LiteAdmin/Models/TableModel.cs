namespace LiteAdmin.Models
{
    using System.Collections.Generic;

    public class TableModel
    {
        public TableModel()
        {
        }

        public TableModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public ICollection<ColumnModel> Columns { get; set; } = new List<ColumnModel>();
    }
}
