namespace LiteAdmin.SqlServer
{
    using System.Collections.Generic;
    using Core;

    public class Record :IRecord
    {
        public ICollection<IColumnValue> Columns { get; } = new List<IColumnValue>();
    }
}
