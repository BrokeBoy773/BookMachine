using BookMachine.Application.Services;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Persistence.Repositories;

namespace BookMachine.API.Extensions
{
    public static class UserServiceExtensions
    {
        public static void AddUserServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
