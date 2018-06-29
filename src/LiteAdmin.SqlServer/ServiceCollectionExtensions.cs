namespace LiteAdmin.SqlServer
{
    using Core;
    using Handlers;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteAdmin(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<ISchemaRepository>(s => new SchemaRepository(connectionString));
            services.AddTransient<IDatabaseRepository>(s => new DatabaseRepository(connectionString));
            services.AddTransient<IApiCallHandler, ApiCallHandler>();
            services.AddTransient<IStaticFileHandler, StaticFileHandler>();
            services.AddTransient<ITableCallHandler, TableCallHandler>();
            services.AddTransient<ISchemaHandler, SchemaHandler>();
            return services;
        }
    }
}
