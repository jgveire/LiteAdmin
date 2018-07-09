    namespace LiteAdmin.Handlers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Http;

    public class LookupCallHandler : JsonHandler, ILookupCallHandler
    {
        private readonly IDatabaseRepository _databaseRepository;

        public LookupCallHandler(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository ?? throw new ArgumentNullException(nameof(databaseRepository));
        }

        public async Task Handle(ITable table)
        {
            if (HttpMethods.IsGet(Request.Method))
            {
                var items = await _databaseRepository.GetLookupItems(table);
                await JsonResponse(items);
            }
            else
            {
                await HttpNotFoundResponse();
            }
        }
    }
}
