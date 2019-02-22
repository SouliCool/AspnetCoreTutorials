using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using SouliCool.Tutorials.Entities;
using Microsoft.EntityFrameworkCore;
using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Repository;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SouliCool.Tutorials.Models;
using Microsoft.AspNetCore.Http;
using SouliCool.Tutorials.LoggerService;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SouliCool.Tutorials
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SouliCool.Tutorials", Version = "v1" });
            });
        }
        public static void ConfigureEF(this IServiceCollection services)
        {
            services.AddDbContext<EntitiesDbContext>(context => 
            {
                context.UseInMemoryDatabase("SouliCoolTutorials"); }
            );

        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
        }
        public static void UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
        public static void UseGlobalExceptionHandling(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Ops! Error"
                    }.ToString());
                });
            });
        }
        
    }   
}
