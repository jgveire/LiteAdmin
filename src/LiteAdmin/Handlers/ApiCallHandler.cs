namespace LiteAdmin.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using LiteAdmin.Extensions;
    using Microsoft.AspNetCore.Http;

    public class ApiCallHandler : JsonHandler, IApiCallHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISchemaRepository _schemaRepository;

        public ApiCallHandler(
            IServiceProvider serviceProvider,
            ISchemaRepository schemaRepository)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
        }

        public async Task Handle(PathString remainingPath)
        {
            var id = GetIdentifier(remainingPath);
            var name = GetControllerName(remainingPath);
            if (string.IsNullOrEmpty(name))
            {
                await HttpNotFoundResponse();
            }
            else if (string.Equals(name, "schema", StringComparison.OrdinalIgnoreCase))
            {
                var schemaHandler = _serviceProvider.GetService<ISchemaHandler>();
                schemaHandler.Context = Context;
                await schemaHandler.Handle();
            }
            else
            {
                var tables = _schemaRepository.GetTables();
                var table = tables.GetTableByName(name);
                if (table == null)
                {
                    await HttpNotFoundResponse();
                }
                else
                {
                    var tableCallHandler = _serviceProvider.GetService<ITableCallHandler>();
                    tableCallHandler.Context = Context;
                    await tableCallHandler.Handle(table, id);
                }
            }
        }

        private string GetControllerName(PathString remainingPath)
        {
            if (remainingPath.HasValue)
            {
                var items = remainingPath.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length >= 1)
                {
                    return items[0];
                }
            }

            return null;
        }

        private string GetIdentifier(PathString remainingPath)
        {
            if (remainingPath.HasValue)
            {
                var items = remainingPath.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (items.Length > 1)
                {
                    return items[1];
                }
            }

            return null;
        }

    }
}
