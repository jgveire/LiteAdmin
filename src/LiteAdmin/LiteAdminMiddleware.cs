namespace LiteAdmin
{
    using System;
    using System.Threading.Tasks;
    using Handlers;
    using Microsoft.AspNetCore.Http;

    public class LiteAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiCallHandler _apiCallHandler;
        private readonly IStaticFileHandler _staticFileHandler;

        public LiteAdminMiddleware(
            RequestDelegate next,
            IApiCallHandler apiCallHandler,
            IStaticFileHandler staticFileHandler)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _apiCallHandler = apiCallHandler ?? throw new ArgumentNullException(nameof(apiCallHandler));
            _staticFileHandler = staticFileHandler ?? throw new ArgumentNullException(nameof(staticFileHandler));
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));

            }
            else if (context.Request.Path.StartsWithSegments("/liteadmin/api", out var remaining))
            {
                return _apiCallHandler.Handle(context, remaining);
            }
            else if (context.Request.Path.StartsWithSegments("/liteadmin", out remaining))
            {
                return _staticFileHandler.Handle(context, remaining);
            }

            return _next(context);
        }
    }
}