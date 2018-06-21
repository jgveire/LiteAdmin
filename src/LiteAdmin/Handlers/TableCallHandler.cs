namespace LiteAdmin.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Http;

    public class TableCallHandler : JsonHandler, ITableCallHandler
    {
        private readonly IDatabaseRepository _databaseRepository;

        public TableCallHandler(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(databaseRepository));
        }

        public async Task Handle(string tableName)
        {
            if (HttpMethods.IsGet(Request.Method))
            {
                var records = await _databaseRepository.GetItemsAsync(tableName, 1);
                await JsonResponse(records);
            }
            else
            {
                await HttpNotFoundResponse();
            }
        }
    }
}
