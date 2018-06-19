namespace LiteAdmin
{
    using System;
    using System.Threading.Tasks;
    using Handlers;
    using Microsoft.AspNetCore.Http;

    public class LiteAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiCallHandler _apiCallHandler = new ApiCallHandler();
        private readonly StaticFileHandler _staticFileHandler = new StaticFileHandler();

        public LiteAdminMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/liteadmin/api"))
            {
                return _apiCallHandler.Handle(context);
            }
            else if (context.Request.Path.StartsWithSegments("/liteadmin", out var remaining))
            {
                return _staticFileHandler.Handle(context, remaining);
            }

            return _next(context);
        }
    }
}