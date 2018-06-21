namespace LiteAdmin.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public static class TableExxtensions
    {
        public static bool ContaintTable(this ICollection<ITable> tables, string tableName)
        {
            if (tables == null)
            {
                return false;
            }

            return tables.Any(e => e.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
