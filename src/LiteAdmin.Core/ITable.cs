namespace LiteAdmin.Core
{
    using System.Collections.Generic;

    public interface ITable
    {
        ICollection<IColumn> Columns { get; }
        string Name { get; }
        string PrimaryKey { get; }
        string Schema { get; }
    }
}