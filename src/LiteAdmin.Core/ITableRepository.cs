namespace LiteAdmin.Core
{
    using System.Collections.Generic;

    public interface ITableRepository
    {
        ICollection<ITable> GetTables();
    }
}
