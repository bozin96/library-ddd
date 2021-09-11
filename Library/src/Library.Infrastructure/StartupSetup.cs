using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    connection => connection.MigrationsAssembly("Library.Web"));

                //if (_env.IsProduction()) return;
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }); // will be created in web project root
    }
}
