namespace LiteAdmin.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using LiteAdmin.Extensions;
    using Microsoft.AspNetCore.Http;

    public class ApiCallHandler : JsonHandler, IApiCallHandler
    {
        private readonly LiteAdminOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISchemaRepository _schemaRepository;

        public ApiCallHandler(
            LiteAdminOptions options,
            IServiceProvider serviceProvider,
            ISchemaRepository schemaRepository)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
        }

        public async Task Handle(PathString remainingPath)
        {
            if (!string.IsNullOrEmpty(_options.Username))
            {
                if (!Context.User.Identity.IsAuthenticated ||
                    !string.Equals(Context.User.Identity.Name, _options.Username, StringComparison.OrdinalIgnoreCase))
                {
                    HttpForbiddenResponse();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(_options.Role))
            {
                if (!Context.User.Identity.IsAuthenticated ||
                    !Context.User.IsInRole(_options.Role))
                {
                    HttpForbiddenResponse();
                    return;
                }
            }

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
            else if (string.Equals(name, "lookup"))
            {
                var tables = _schemaRepository.GetTables();
                var table = tables.GetTableByName(id);
                if (table == null)
                {
                    await HttpNotFoundResponse();
                }
                else
                {
                    var lookupCallHandler = _serviceProvider.GetService<ILookupCallHandler>();
                    lookupCallHandler.Context = Context;
                    await lookupCallHandler.Handle(table);
                }
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
