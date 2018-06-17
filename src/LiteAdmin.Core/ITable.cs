namespace LiteAdmin.Core
{
    using System.Collections.Generic;

    public interface ITable
    {
        string Name { get; }
        string Schema { get; }
        ICollection<IColumn> Columns { get; }
    }
}