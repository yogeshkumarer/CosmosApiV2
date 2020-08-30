using CosmosApi.DataSetup;
using CosmosApi.DbClient;
using CosmosApi.Extensions;
using CosmosApi.Handlers;
using CosmosApi.Repository;
using CosmosApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Mime;

namespace CosmosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureVersioning();

            services.AddDbConfiguration(Configuration);

            AddServicesForCustomerManagement(services);

            services.AddControllers().ConfigureApiBehaviorOptions(o =>
            {
                o.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<CustomErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private static void AddServicesForCustomerManagement(IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<ICosmosDbClient, CosmosDbClient>();
            services.AddSingleton<ICosmosRepository, CosmosRepository>();
            services.AddSingleton<IContainerNameService, ContainerNameService>();
            services.AddSingleton<IQueryBuilder, QueryBuilder>();
        }
    }
}
