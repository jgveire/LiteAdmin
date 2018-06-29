namespace LiteAdmin.Handlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Models;

    public class SchemaHandler : JsonHandler, ISchemaHandler
    {
        private readonly ISchemaRepository _schemaRepository;

        public SchemaHandler(ISchemaRepository schemaRepository)
        {
            _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
        }

        public Task Handle()
        {
            var tables = _schemaRepository
                .GetTables()
                .Select(t => new TableModel
                {
                    Name = t.Name,
                    Columns = t.Columns.Select(c => new ColumnModel
                    {
                        Name = c.Name,
                        DataType = c.DataType.Name,
                        DefaultValue = c.DefaultValue?.ToString(),
                        IsNullable = c.IsNullable,
                        MaxLength = c.MaxLength,
                        IsPrimaryKey = c.IsPrimaryKey
                    }).ToList()
                });

            return JsonResponse(tables);
        }
    }
}