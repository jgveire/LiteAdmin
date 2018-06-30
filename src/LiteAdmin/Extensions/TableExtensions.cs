namespace LiteAdmin.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public static class TableExtensions
    {
        public static ITable GetTableByName(this ICollection<ITable> tables, string tableName)
        {
            return tables?.FirstOrDefault(e => e.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
