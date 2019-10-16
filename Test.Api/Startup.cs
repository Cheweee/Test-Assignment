using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Test.Data;
using Test.Data.Enumerations;
using Test.Data.Interfaces;
using Test.Services;
using Test.Services.ScraperStrategies;
using Test.Shared;
using Test.Utilities.Extensibility;

namespace Test.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("*");
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connectionString = AppSettings.MSSqlServerConnectionString;

            services.AddScoped(provider =>
            {
                var logger = provider.GetService<ILogger<IDaoFactory>>();
                return DaoFactories.GetFactory(DataProvider.MSSql, connectionString, logger);
            });

            services.AddScoped(provider =>
            {
                var daoFactory = provider.GetService<IDaoFactory>();
                return new InstructorService(daoFactory.InstructorDao);
            });

            services.AddScoped(provider => new AllUrlsScraperStrategy());
            services.AddScoped<IAppendStrategyExtensionsReader>(provider => provider.GetService<AllUrlsScraperStrategy>());

            services.AddScoped(provider => new ImagesSourcesScraperStrategy());
            services.AddScoped<IAppendStrategyExtensionsReader>(provider => provider.GetService<ImagesSourcesScraperStrategy>());
            
            services.AddScoped(provider => new DoesNotContainsProfileScraperStrategy());
            services.AddScoped<IExcludeStrategyExtensionsReader>(provider => provider.GetService<DoesNotContainsProfileScraperStrategy>());

            services.AddScoped(provider =>
            {
                var logger = provider.GetService<ILogger<ScraperService>>();
                return new ScraperService(logger, provider.ComposeAppendStrategyReaders(), provider.ComposeExcludeStrategyReaders());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHttpsRedirection();
                app.UseHsts();
            }
            app.UseCors();

            app.UseMvc();
        }
    }
}
