using BookMachine.Core.Models;

namespace BookMachine.Core.Interfaces.Persistence.Repositories
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllBooksAsync();
        public Task<Book?> GetBookByIdAsync(Guid bookId);

        public Task<Guid> CreateBookAsync(Book book);

        public Task<Guid> UpdateBookAsync(Guid bookId, Book book);

        public Task<Guid> DeleteBookAsync(Guid bookId);
    }
}
