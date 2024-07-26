using BookMachine.Application.Services;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Persistence.Repositories;

namespace BookMachine.API.Extensions
{
    public static class AuthorServiceExtensions
    {
        public static void AddAuthorServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
        }
    }
}
