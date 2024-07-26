using BookMachine.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static void AddDatabaseServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookMachineDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(BookMachineDbContext)));
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }
    }
}
