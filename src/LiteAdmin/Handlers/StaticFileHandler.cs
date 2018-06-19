namespace LiteAdmin.Handlers
{
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class StaticFileHandler : HandlerBase, IStaticFileHandler
    {
        public Task Handle(HttpContext context, PathString remainingPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = GetResourceName(remainingPath);

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return PageNotFoundAsync(context);
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string contentType = GetContentType(resourceName);
                    string content = reader.ReadToEnd();
                    var bytes = Encoding.UTF8.GetBytes(content);
                    context.Response.ContentType = contentType;
                    return context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        }

        private async Task PageNotFoundAsync(HttpContext context)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = GetResourceName("/page-not-found.html");

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var contentType = GetContentType(resourceName);
                var content = await reader.ReadToEndAsync();
                var bytes = Encoding.UTF8.GetBytes(content);
                context.Response.ContentType = contentType;
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        private static string GetResourceName(PathString remainingPath)
        {
            if (remainingPath.HasValue)
            {
                var resourcePath = "LiteAdmin.StaticFiles";
                var filePath = remainingPath.Value.Replace('/', '.').ToLower(CultureInfo.InvariantCulture);
                return $"{resourcePath}{filePath}";
            }

            return "LiteAdmin.StaticFiles.index.html";
        }
    }
}
