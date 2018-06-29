namespace LiteAdmin.Core
{
    using System.Collections.Generic;

    public interface ISchemaRepository
    {
        ICollection<ITable> GetTables();
    }
}
