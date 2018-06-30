    namespace LiteAdmin.Handlers
{
    using System;
    using System.IO;
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

        public async Task Handle(ITable table, string id)
        {
            if (HttpMethods.IsGet(Request.Method) && !string.IsNullOrEmpty(id))
            {
                var records = await _databaseRepository.GetItemAsync(table, id);
                await JsonResponse(records);
            }
            else if (HttpMethods.IsGet(Request.Method))
            {
                var records = await _databaseRepository.GetItemsAsync(table);
                await JsonResponse(records);
            }
            else if (HttpMethods.IsPut(Request.Method) && !string.IsNullOrEmpty(id))
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var json = reader.ReadToEnd();
                    var dictionary = DeserializeJson(json);
                    await _databaseRepository.UpdateItemAsync(table, id, dictionary);
                    HttpNoContentResponse();
                }
            }
            else if (HttpMethods.IsDelete(Request.Method) && !string.IsNullOrEmpty(id))
            {
                await _databaseRepository.DeleteItemAsync(table, id);
                HttpNoContentResponse();
            }
            else if (HttpMethods.IsPost(Request.Method))
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var json = reader.ReadToEnd();
                    var dictionary = DeserializeJson(json);
                    await _databaseRepository.InsertItemAsync(table, dictionary);
                    HttpNoContentResponse();
                }
            }
            else
            {
                await HttpNotFoundResponse();
            }
        }
    }
}
