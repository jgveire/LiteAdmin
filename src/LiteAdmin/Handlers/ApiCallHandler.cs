namespace LiteAdmin.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using LiteAdmin.Extensions;
    using Microsoft.AspNetCore.Http;

    public class ApiCallHandler : JsonHandler, IApiCallHandler
    {
        private readonly ITableCallHandler _tableCallHandler;
        private readonly ISchemaHandler _schemaHandler;
        private readonly ITableRepository _tableRepository;

        public ApiCallHandler(
            ITableCallHandler tableCallHandler,
            ISchemaHandler schemaHandler,
            ITableRepository tableRepository)
        {
            _tableCallHandler = tableCallHandler ?? throw new ArgumentNullException(nameof(tableCallHandler));
            _schemaHandler = schemaHandler ?? throw new ArgumentNullException(nameof(schemaHandler));
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
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
                _schemaHandler.Context = Context;
                return _schemaHandler.Handle();
            }

            var tables = _tableRepository.GetTables();
            if (!tables.ContaintTable(name))
            {
                return HttpNotFoundResponse();
            }

            _tableCallHandler.Context = Context;
            return _tableCallHandler.Handle(name, id);
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
