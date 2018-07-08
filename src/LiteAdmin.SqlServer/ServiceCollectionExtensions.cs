namespace LiteAdmin.SqlServer
{
    using System.Collections.Generic;
    using Core;
    using Handlers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteAdmin(this IServiceCollection services, string connectionString, LiteAdminOptions options)
        {
            options = options ?? new LiteAdminOptions();
            options.Tables = options.Tables ?? new List<string>();

            services.AddTransient(s => options);
            services.AddTransient<ISchemaRepository>(s => new SchemaRepository(connectionString, options.Tables));
            services.AddTransient<IDatabaseRepository>(s => new DatabaseRepository(connectionString));
            services.AddTransient<IApiCallHandler, ApiCallHandler>();
            services.AddTransient<IStaticFileHandler, StaticFileHandler>();
            services.AddTransient<ITableCallHandler, TableCallHandler>();
            services.AddTransient<ISchemaHandler, SchemaHandler>();
            return services;
        }
    }
}

