using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Filters;
using System;
using System.IO;

namespace SouliCool.Tutorials
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration("../../../nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Configure swagger with extension method
            services.ConfigureSwagger();
            //Configure EF with In-memory database 
            services.ConfigureEF();
            //Configure DI from extension method 
            services.ConfigureRepositoryWrapper();
            //Configure Logger
            services.ConfigureLoggerService();
            //add Model validation filter
            services.AddScoped<ModelValidationAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //add global exception handler
            app.UseGlobalExceptionHandling(logger);

            app.UseMvc();            
            //add swagger with extension method
            app.UseSwagger();           
            
        }
    }
}
