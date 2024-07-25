using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using BookMachine.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            
            return BookMapping.FromBookEntityListToBookList(bookEntities);
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

            return BookMapping.FromBookEntityToBook(bookEntities, bookEntity!);
        }

        public async Task<List<Book>> GetBookByFilterAsync(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<BookEntity> bookEntityQuery = _context.BookEntities
                .AsNoTracking()
                .Where(b => string.IsNullOrWhiteSpace(search) || b.Title.ToLower().Contains(search.ToLower()))
                .Include(b => b.Author);

            Expression<Func<BookEntity, object>> selectorKey;

            switch (sortItem)
            {
                case "title":
                    selectorKey = bookEntity => bookEntity.Title;
                    break;

                case "id":
                    selectorKey = bookEntity => bookEntity.AuthorId;
                    break;

                default:
                    goto case "id";
            }

            switch (sortOrder)
            {
                case "desc":
                    bookEntityQuery = bookEntityQuery.OrderByDescending(selectorKey);
                    break;

                case "asc":
                    bookEntityQuery = bookEntityQuery.OrderBy(selectorKey);
                    break;

                default:
                    goto case "asc";
            }

            List<BookEntity> bookEntities = await bookEntityQuery.ToListAsync();

            return BookMapping.FromBookEntityListToBookList(bookEntities);
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
            BookEntity bookEntity = BookMapping.FromBookToBookEntity(book);

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
