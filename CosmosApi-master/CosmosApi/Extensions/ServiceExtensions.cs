using CosmosApi.DbClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbConfiguration>(o =>
            {
                o.DbConnection = configuration[Constants.CosmosConnection];
                o.DbName = configuration[Constants.CosmosDbName];
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });
        }        
    }
}
