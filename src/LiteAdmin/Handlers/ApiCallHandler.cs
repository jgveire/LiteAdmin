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
        private readonly ITableRepository _tableRepository;

        public ApiCallHandler(
            ITableCallHandler tableCallHandler,
            ITableRepository tableRepository)
        {
            _tableCallHandler = tableCallHandler ?? throw new ArgumentNullException(nameof(tableCallHandler));
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
        }

        public Task Handle(PathString remainingPath)
        {
            var tableName = GetTableName(remainingPath);
            if (!string.IsNullOrEmpty(tableName))
            {
                var tables = _tableRepository.GetTables();
                if (tables.ContaintTable(tableName))
                {
                    _tableCallHandler.Context = Context;
                    return _tableCallHandler.Handle(tableName);
                }
            }

            return HttpNotFoundResponse();
        }

        private string GetTableName(PathString remainingPath)
        {
            if (remainingPath.HasValue)
            {
                var startIndex = remainingPath.Value.IndexOf('/');
                var nextIndex = remainingPath.Value.IndexOf('/', startIndex + 1);
                if (startIndex != -1 && nextIndex != -1)
                {
                    return remainingPath.Value.Substring(startIndex + 1, nextIndex - startIndex - 1);
                }
                else if (startIndex != -1)
                {
                    return remainingPath.Value.Substring(startIndex + 1, remainingPath.Value.Length - startIndex - 1);
                }
            }

            return null;
        }
    }
}
