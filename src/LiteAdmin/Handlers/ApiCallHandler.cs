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

        public Task Handle(PathString remainingPath)
        {
            var id = GetIdentifier(remainingPath);
            var name = GetControllerName(remainingPath);
            if (string.IsNullOrEmpty(name))
            {
                return HttpNotFoundResponse();
            }
            else if (string.Equals(name, "schema", StringComparison.OrdinalIgnoreCase))
            {
                var schemaHandler = _serviceProvider.GetService<ISchemaHandler>();
                schemaHandler.Context = Context;
                return schemaHandler.Handle();
            }

            var tables = _schemaRepository.GetTables();
            var table = tables.GetTableByName(name);
            if (table == null)
            {
                return HttpNotFoundResponse();
            }

            var tableCallHandler = _serviceProvider.GetService<ITableCallHandler>();
            tableCallHandler.Context = Context;
            return tableCallHandler.Handle(table, id);
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
