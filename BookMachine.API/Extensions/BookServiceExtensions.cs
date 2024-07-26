using BookMachine.Application.Services;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Persistence.Repositories;

namespace BookMachine.API.Extensions
{
    public static class BookServiceExtensions
    {
        public static void AddBookServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
