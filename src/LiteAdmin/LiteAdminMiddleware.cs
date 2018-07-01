namespace LiteAdmin
{
    using System;
    using System.Threading.Tasks;
    using Handlers;
    using LiteAdmin.Extensions;
    using Microsoft.AspNetCore.Http;

    public class LiteAdminMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public LiteAdminMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));

            }
            else if (context.Request.Path.StartsWithSegments("/liteadmin/api", out var remaining))
            {
                var apiCallHandler = _serviceProvider.GetService<IApiCallHandler>();
                apiCallHandler.Context = context;
                await apiCallHandler.Handle(remaining);
            }
            else if (context.Request.Path.StartsWithSegments("/liteadmin", out remaining))
            {
                var staticFileHandler = _serviceProvider.GetService<IStaticFileHandler>();
                await staticFileHandler.Handle(context, remaining);
            }
            else
            {
                await _next(context);
            }
        }
    }
}