namespace LiteAdmin
{
    using System;
    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLiteAdmin(
            this IApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return builder.UseMiddleware<LiteAdminMiddleware>();
        }
    }
}
