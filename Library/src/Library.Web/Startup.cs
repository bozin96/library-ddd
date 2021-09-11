﻿using Ardalis.ListStartupServices;
using Autofac;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Core;
using Library.Infrastructure;
using Library.Infrastructure.Data;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using Library.Web.Configurations;
using Library.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Library.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            Configuration = config;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //string connectionString = Configuration.GetConnectionString("SqliteConnection");
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    connection => connection.MigrationsAssembly("Library.Web"));

                if (_env.IsProduction()) return;
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });


            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddMapperSetup();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddTransient<ILibraryService, LibraryService>();

            // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                // optional - default path to view services is /listallservices - recommended to choose your own path
                config.Path = "/listservices";
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultCoreModule());
            builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1"));
        }
    }
}
