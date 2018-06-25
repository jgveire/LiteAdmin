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

        public async Task Handle(string tableName, string id)
        {
            if (HttpMethods.IsGet(Request.Method) && !string.IsNullOrEmpty(id))
            {
                var records = await _databaseRepository.GetItemAsync(tableName, id);
                await JsonResponse(records);
            }
            else if (HttpMethods.IsGet(Request.Method))
            {
                var records = await _databaseRepository.GetItemsAsync(tableName, 1);
                await JsonResponse(records);
            }
            else if (HttpMethods.IsPut(Request.Method) && !string.IsNullOrEmpty(id))
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var json = reader.ReadToEnd();
                    var dictionary = DeserializeJson(json);
                    await _databaseRepository.UpdateItemAsync(tableName, id, dictionary);
                    HttpNoContentResponse();
                }
            }
            else if (HttpMethods.IsDelete(Request.Method) && !string.IsNullOrEmpty(id))
            {
                await _databaseRepository.DeleteItemAsync(tableName, id);
                HttpNoContentResponse();
            }
            else if (HttpMethods.IsPost(Request.Method))
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var json = reader.ReadToEnd();
                    var dictionary = DeserializeJson(json);
                    await _databaseRepository.InsertItemAsync(tableName, dictionary);
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
