namespace LiteAdmin.Handlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Models;

    public class SchemaHandler : JsonHandler, ISchemaHandler
    {
        private readonly ITableRepository _tableRepository;

        public SchemaHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public Task Handle()
        {
            var tables = _tableRepository
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
                        MaxLength = c.MaxLength
                    }).ToList()
                });

            return JsonResponse(tables);
        }
    }
}