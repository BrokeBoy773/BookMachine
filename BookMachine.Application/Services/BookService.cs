using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;

namespace BookMachine.Application.Services
{
    public class BookService(IBookRepository bookRepository) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<List<Book>> GetBookByFilterAsync(string? search, string? sortItem, string? sortOrder)
        {
            return await _bookRepository.GetBookByFilterAsync(search, sortItem, sortOrder);
        }

        public async Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
            return await _bookRepository.CreateBookAsync(book);
        }

        public async Task<Guid> UpdateBookAsync(Guid bookId, Book book)
        {
            return await _bookRepository.UpdateBookAsync(bookId, book);
        }

        public async Task<Guid> DeleteBookAsync(Guid bookId)
        {
            return await _bookRepository.DeleteBookAsync(bookId);
        }

    }
}
