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
                .Include(b => b.Author)
                .ToListAsync();

            List<Book> books = [];

            foreach (BookEntity bookEntity in bookEntities)
            {
                Author author = Author.Create(bookEntity.AuthorId, bookEntity!.Author!.Name, []).Author;

                foreach (var bookEnt in bookEntity!.Author!.Books)
                {
                    books.Add(Book.Create(bookEnt.BookId, bookEnt.Title, bookEnt.AuthorId, author).Book);
                }
            }
            
            return books;
        }
        public async Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            BookEntity? bookEntity = await _context.BookEntities
                .AsNoTracking()
                .Where(b => b.BookId == bookId)
                .Include(b => b.Author)
                .FirstOrDefaultAsync();

            List<BookEntity> bookEntities = await _context.BookEntities
                .AsNoTracking()
                .Where(b => b.AuthorId == bookEntity!.AuthorId)
                .Include(b => b.Author)
                .ToListAsync();

            Author? author = null;

            List<Book> books = [];
            author = Author.Create(bookEntity!.AuthorId, bookEntity!.Author!.Name, books).Author;

            foreach (var bookEnt in bookEntities)
            {
                books.Add(Book.Create(bookEnt.BookId, bookEnt.Title, bookEnt.AuthorId, author).Book);
            }

            Book? book = Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book;

            return book;
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
            BookEntity bookEntity = new()
            {
                BookId = book.BookId,
                Title = book.Title,

                AuthorId = book.AuthorId
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
