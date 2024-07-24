using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.Persistence.Repositories
{
    public class AuthorRepository(BookMachineDbContext context) : IAuthorRepository
    {
        private readonly BookMachineDbContext _context = context;

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            List<AuthorEntity> authorEntities = await _context.AuthorEntities
                .AsNoTracking()
                .Include(a => a.Books)
                .ToListAsync();

            List<Author> authors = [];

            foreach (var authorEntity in authorEntities)
            {
                if (authorEntity.Books.Count != 0)
                {
                    List<Book> books = [];
                    Author author = Author.Create(authorEntity.AuthorId, authorEntity.Name, books).Author;

                    foreach (var bookEntity in authorEntity.Books)
                    {
                        books.Add(Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book);
                    }

                    authors.Add(author);
                }
                else
                {
                    Author author = Author.Create(authorEntity.AuthorId, authorEntity.Name, []).Author;
                    authors.Add(author);
                }
            }
            
            return authors;
        }
        public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
        {
            AuthorEntity? authorEntity = await _context.AuthorEntities
                .AsNoTracking()
                .Where(a => a.AuthorId == authorId)
                .Include(a => a.Books)
                .FirstOrDefaultAsync();

            Author? author = null;

            if (authorEntity!.Books.Count != 0)
            {
                List<Book> books = [];
                author = Author.Create(authorEntity.AuthorId, authorEntity.Name, books).Author;

                foreach (var bookEntity in authorEntity!.Books)
                {
                    books.Add(Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book);
                }
            }
            else
            {
                author = Author.Create(authorEntity.AuthorId, authorEntity.Name, []).Author;
            }

            return author;
        }

        public async Task<Guid> CreateAuthorAsync(Author author)
        {
            AuthorEntity authorEntity = new()
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
            };

            await _context.AddAsync(authorEntity);
            await _context.SaveChangesAsync();

            return authorEntity.AuthorId;
        }

        public async Task<Guid> UpdateAuthorAsync(Guid authorId, Author author)
        {
            await _context.AuthorEntities.Where(a => a.AuthorId == authorId)
                .ExecuteUpdateAsync(x => x.SetProperty(y => y.Name, y => author.Name));

            return authorId;
        }

        public async Task<Guid> DeleteAuthorAsync(Guid authorId)
        {
            await _context.AuthorEntities.Where(a => a.AuthorId == authorId)
                .ExecuteDeleteAsync();

            return authorId;
        }
    }
}
