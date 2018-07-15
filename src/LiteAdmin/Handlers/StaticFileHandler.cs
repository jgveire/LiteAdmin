namespace LiteAdmin.Handlers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class StaticFileHandler : HandlerBase, IStaticFileHandler
    {
        public LiteAdminOptions Options { get; }

        public StaticFileHandler(LiteAdminOptions options)
        {
            Options = options;
        }

        public async Task Handle(HttpContext context, PathString remainingPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = GetResourceName(remainingPath);

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    await PageNotFoundAsync(context);
                }

                byte[] bytes;
                string contentType = GetContentType(resourceName);
                if (string.Equals(resourceName, "LiteAdmin.StaticFiles.index.html", StringComparison.OrdinalIgnoreCase))
                {

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        content = InjectCustomFiles(content);
                        bytes = Encoding.UTF8.GetBytes(content);
                        context.Response.ContentType = contentType;
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                else
                {

                    context.Response.ContentType = contentType;

                    bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, Convert.ToInt32(stream.Length));
                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                }
            }
        }

        private string InjectCustomFiles(string content)
        {
            if (!string.IsNullOrEmpty(Options?.CustomCssUrl))
            {
                content = content.Replace("</head>", $"<link rel=\"stylesheet\" type=\"text/css\" href=\"{Options.CustomCssUrl}\"></head>");
            }

            if (!string.IsNullOrEmpty(Options?.CustomJavaScriptUrl))
            {
                content = content.Replace("</head>", $"<script type=\"text/javascript\" src=\"{Options.CustomJavaScriptUrl}\"></script></head>");
            }

            return content;
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
                if (remainingPath.Value.Equals("/"))
                {
                    return "LiteAdmin.StaticFiles.index.html";
                }

                var resourcePath = "LiteAdmin.StaticFiles";
                var filePath = remainingPath.Value.Replace('/', '.').ToLower(CultureInfo.InvariantCulture);
                return $"{resourcePath}{filePath}";
            }

            return "LiteAdmin.StaticFiles.index.html";
        }
    }
}
