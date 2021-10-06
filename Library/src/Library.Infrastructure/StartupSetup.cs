using Hangfire;
using Library.Infrastructure.Data;
using Library.SharedKernel.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Library.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString, bool isProduction)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    connection => connection.MigrationsAssembly("Library.Web"));

                if (isProduction) return;
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }); // will be created in web project root
        }

        public static void AddHangfire(this IServiceCollection services, string connectionString)
        {
            var backgroundJobServerOptions = new BackgroundJobServerOptions
            {
                SchedulePollingInterval = TimeSpan.FromSeconds(10),
                WorkerCount = 12
            };

            services.AddHangfireServer((backgroundJobServerOptions)=> { });
            services.AddHangfire(configuration =>
                {
                    configuration.UseSqlServerStorage(connectionString);
                    configuration.UseMediatR();
                });
        }
    }
}
