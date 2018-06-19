namespace LiteAdmin.SqlServer
{
    using Core;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteAdmin(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<ITableRepository>(s => new TableRepository(connectionString));
            return services;
        }
    }
}
