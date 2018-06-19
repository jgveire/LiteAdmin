namespace LiteAdmin.Handlers
{
    using Microsoft.AspNetCore.StaticFiles;

    public class HandlerBase
    {
        protected FileExtensionContentTypeProvider ContentTypeProvider { get; } = new FileExtensionContentTypeProvider();

        protected string GetContentType(string fileName)
        {
            if (ContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                return contentType;
            }

            return "application/octet-stream";
        }


        protected string GetErrorJson(int code, string message)
        {
            return $"{{\"error\":{{\"code\":{code},\"message\":\"{message}\",\"data\":null}}}}";
        }
    }
}
