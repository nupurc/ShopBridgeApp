using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NLog;
using ShopBridge.Services.Interfaces;
using ShopBridgeApp.Repository;
using ShopBridgeApp.Repository.Interface;
using ShopBridgeApp.Services;
using System.Net;

namespace ShopBridgeAppApi
{
    public class Startup
    {
        private readonly Microsoft.Extensions.Logging.ILogger logger;
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            logger = (Microsoft.Extensions.Logging.ILogger)LogManager.GetCurrentClassLogger();
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            // Add the Pascal Notation formatting
            services.AddControllers().AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            //services.ConfigureAll();

            // Add the Swagger Definition to the project
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ShopBridgeApp API",
                    Version = "v1",
                });
            });
            services.AddTransient<IShopBridgeAppService, ShopBridgeAppService>();
            services.AddTransient<IShopBridgeItemRepository, ShopBridgeItemRepository>();
            // End of the Swagger Definition to the project

            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Setup global exception handler middleware
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    if (exception != null)
                    {
                        logger.LogError($"{exception.Error.Message}\nStackTrace:\n{exception.Error.StackTrace}");

                        var err = new
                        {
                            context.Response.StatusCode,
                            exception.Error.Message,
                            exception.Error.StackTrace,
                        };

                        await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(err)).ConfigureAwait(false);
                    }
                });
            });
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // Add the Swagger Definition to the project
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopBridgeApp API");
            });

            // End of the Swagger Definition to the project
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
