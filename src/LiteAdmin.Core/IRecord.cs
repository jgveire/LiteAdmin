namespace LiteAdmin.Core
{
    using System.Collections.Generic;

    public interface IRecord
    {
        ICollection<IColumnValue> Columns { get; }
    }
}