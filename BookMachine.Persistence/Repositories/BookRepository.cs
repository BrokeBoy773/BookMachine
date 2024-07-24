using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.Persistence.Repositories
{
    public class BookRepository(BookMachineDbContext context) : IBookRepository
    {
        private readonly BookMachineDbContext _context = context;
        
        public async Task<List<Book>> GetAllBooksAsync()
        {
            List<BookEntity> bookEntities = await _context.BookEntities
                .AsNoTracking()
                .ToListAsync();

            List<Book> books = bookEntities
                .Select(b => Book.Create(b.BookId, b.Title).Book)
                .ToList();

            return books;
        }
        public async Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            BookEntity? bookEntity = await _context.BookEntities
                .AsNoTracking()
                .Where(b => b.BookId == bookId)
                .FirstOrDefaultAsync();
            
            Book? book = Book.Create(bookEntity!.BookId, bookEntity.Title).Book;

            return book;
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
            BookEntity bookEntity = new()
            {
                BookId = book.BookId,
                Title = book.Title,
            };

            await _context.BookEntities.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.BookId;
        }

        public async Task<Guid> UpdateBookAsync(Guid bookId, Book book)
        {
            await _context.BookEntities.Where(b => b.BookId == bookId)
                .ExecuteUpdateAsync(x => x.SetProperty(y => y.Title, y => book.Title));

            return bookId;
        }

        public async Task<Guid> DeleteBookAsync(Guid bookId)
        {
            await _context.BookEntities.Where(b => b.BookId == bookId)
                .ExecuteDeleteAsync();

            return bookId;
        }
    }
}
